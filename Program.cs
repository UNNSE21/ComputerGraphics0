// See https://aka.ms/new-console-template for more information

using ComputerGraphics0.Filters;
using ComputerGraphics0.Filters.Global;
using ComputerGraphics0.Filters.Pixel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;
using ComputerGraphics0.Filters.Kernel;
using ComputerGraphics0.Filters.Kernel.MathMorph;

namespace ComputerGraphics0;

public static class Program
{   
    // args: 
    // 0 - путь к картинке (если не существует - берем из stdin).
    //     Поскольку валидность существующего файла проверяется только на этапе загрузки в либу - если файл не картинка
    //      - выходим с ошибкой (иначе либо цикл и колхоз с флагами, либо goto. Оба плохо)
    // 1 - имя фильтра
    // 2 - сохранять исходник (true|false). 
    // 3 и далее - параметры фильтра
    // Для значения по умолчанию используем символ '-' (если путь тоже дефис - берем его из stdin)
    public static void Main(string[] args)
    { 
        IImageFilter filter;
        Image<Argb32> input;
        string inputPath = "";
        string filterName = "";
        bool saveSource = true;
        if (args.Length < 2)
        {
            Console.Error.WriteLine("You must enter a path and a filter name as arguments");
            return;
        }

        inputPath = args[0];
        filterName = args[1];
        if (args.Length >= 3)
        {
            if (!Boolean.TryParse(args[2], out saveSource))
            {
                if (args[2] == "-")
                {
                    saveSource = true;
                }
                else
                {
                    Console.Error.WriteLine("Expected true or false value as third argument");
                    return;
                }
            }
        }

        try
        {
            filter = GetFilterByName(filterName, args.Skip(3).ToArray());
        }
        catch (IndexOutOfRangeException)
        {
            Console.Error.WriteLine("Not enough parameters for this filter");
            return;
        }
        catch (NotSupportedException)
        {
            Console.Error.WriteLine("Invalid filter option");
            return;
        }
        catch(Exception ex) when (ex is FormatException || ex is OverflowException)
        {
            Console.Error.WriteLine("Not enough parameters for this filter or some are invalid");
            return;
        }
        ValidateInputPath(ref inputPath);
        try
        {
            input = Image.Load<Argb32>(inputPath);
        }
        catch (UnknownImageFormatException)
        {
            Console.Error.WriteLine("Could not load an image. Either file is not an image or it's format is unsupported");
            return;
        }

        var result = filter.Process(input);
        var resultPath = $"{Path.Join(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath))}_{filter.Name}.png";
        result.SaveAsPng(resultPath);
        if (!saveSource)
        {
            File.Delete(inputPath);
        }
        Console.Write(resultPath);
    }

    private static IImageFilter GetFilterByName(string name, params string[] filterArgs)
    {
        IImageFilter filter;
        int arg0;
        switch (name)
        {
            case "invert":
                filter = new InversionFilter();
                break;
            case "gray":
                filter = new GrayscaleFilter();
                break;
            case "sepia":
                filter = new SepiaFilter(ParseArg(filterArgs, 0, 30f, Single.TryParse));
                break;
            case "inc":
                filter = new IncreaseBrightnessFilter(
                    ParseArg(filterArgs, 0, 50, Int32.TryParse));
                break;
            case "shrooms":
                filter = new ShroomsFilter();
                break;
            case "opening":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new OpeningFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "closing":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new ClosingFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "erose":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new ErosionFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "dilate":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new DilationFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "tophat":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new TopHatFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            
            case "blackhat":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new BlackHatFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "morph_gradient":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new GranientFilter(GenerateCircleMask(arg0), (arg0, arg0));
                break;
            case "inner_border":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new InnerBorderFilter(GenerateCircleMask(arg0), (arg0, arg0), 
                    ParseArg(filterArgs, 1, 127, Int32.TryParse));
                break;
            case "outer_border":
                arg0 = ParseArg(filterArgs, 0, 10, Int32.TryParse);
                filter = new OuterBorderFilter(GenerateCircleMask(arg0), (arg0, arg0), 
                    ParseArg(filterArgs, 1, 127, Int32.TryParse));
                break;
            case "binary":
                filter = new BinarizationFilter(ParseArg(filterArgs, 0, 127, Int32.TryParse));
                break;
            case "avg_blur":
                filter = new AverageBlur(ParseArg(filterArgs, 0, 3, Int32.TryParse));
                break;
            case "gauss_blur":
                filter = new GaussBlur(ParseArg(filterArgs, 0, 3, Int32.TryParse),
                    ParseArg(filterArgs, 1, 10f, Single.TryParse)
                );
                break;
            case "sobel":
                filter = new SobelFilter();
                break;
            default:
                throw new NotSupportedException();
        }

        return filter;
    }
    
    delegate bool TryParseHandler<T>(string value, out T result);
    private static T ParseArg<T>(string[] args, int index, T defaultValue, TryParseHandler<T> handler)
    {
        T arg;
        if (args.Length > index && handler(args[index], out T arg0Out))
            arg = arg0Out;
        else
            arg = defaultValue;
        return arg;
    }
    
    private static bool[,] GenerateCircleMask(int radius)
    {
        var result = new bool[radius * 2 + 1, radius * 2 + 1];
        for (int i = 0; i < result.GetLength(0); ++i)
        {
            for (int j = 0; j < result.GetLength(1); ++j)
            {
                result[i, j] = (i - radius) * (i - radius) + (j - radius) * (j - radius) <= radius*radius;
            }
        }
        return result;
    }
    private static bool[,] GenerateRectMask(int radius)
    {
        var result = new bool[radius * 2 + 1, radius * 2 + 1];
        for (int i = 0; i < result.GetLength(0); ++i)
        {
            for (int j = 0; j < result.GetLength(1); ++j)
            {
                result[i, j] = true;
            }
        }
        return result;
    }

    private static void ValidateInputPath(ref string path)
    {
        if (path == "-")
        {
            path = Console.ReadLine();
        }
        while (String.IsNullOrEmpty(path) ||  path == "-" || !File.Exists(path))
        {
            Console.Error.WriteLine($"Input file ({path}) does not exist. Reading from stdin");
            path = Console.ReadLine();
            Console.WriteLine(path);
        }
    }
}
