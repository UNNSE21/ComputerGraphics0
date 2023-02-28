// See https://aka.ms/new-console-template for more information

using ComputerGraphics0.Filters;
using ComputerGraphics0.Filters.Global;
using ComputerGraphics0.Filters.Pixel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0;

public static class Program
{   
    // args: 
    // 0 - имя фильтра
    // 1 - сохранять исходник (true|false). Если не bool - пытаемся прочитать путь (см. 2) 
    // 2 - путь к картинке (если нет или не существует - берем из stdin).
    //     Поскольку валидность существующего файла проверяется только на этапе загрузки в либу - если файл не картинка
    //      - выходим с ошибкой (иначе либо цикл и колхоз с флагами, либо goto. Оба плохо)
    public static void Main(string[] args)
    {
        IImageFilter filter;
        Image<Argb32> input;
        string inputPath = "";
        bool saveSource = true;
        if (args.Length < 1)
        {
            Console.Error.WriteLine("You must enter a filter name as argument");
            return;
        }
        if (args.Length == 1)
        {
            inputPath = Console.ReadLine();
            ValidateInputPath(ref inputPath);
        }
        if (args.Length >= 2)
        {
            if (bool.TryParse(args[1], out saveSource))
            {
                inputPath = args.Length < 3 ? Console.ReadLine() : args[2];
                ValidateInputPath(ref inputPath);
            }
            else
            {
                saveSource = true;
                inputPath = args[1];
                ValidateInputPath(ref inputPath);
            }
        }
        try
        {
            input = Image.Load<Argb32>(inputPath);
        }
        catch (UnknownImageFormatException)
        {
            Console.Error.WriteLine("Could not load an image. Either file is not an image or it's format is unsupported");
            return;
        }
        try
        {
            filter = GetFilterByName(args[0]);
        }
        catch (NotSupportedException)
        {
            Console.Error.WriteLine("Invalid filter option");
            return;
        }
        filter.Process(input);
        var resultPath = $"{Path.Join(Path.GetDirectoryName(inputPath), Path.GetFileNameWithoutExtension(inputPath))}_{filter.Name}.png";
        input.SaveAsPng(resultPath);
        if (!saveSource)
        {
            File.Delete(inputPath);
        }
        Console.Write(resultPath);
    }

    private static IImageFilter GetFilterByName(string name)
    {
        IImageFilter filter;
        switch (name)
        {
            case "inv":
                filter = new InversionFilter();
                break;
            case "gray":
                filter = new GrayscaleFilter();
                break;
            case "sepia":
                filter = new SepiaFilter();
                break;
            case "inc":
                filter = new IncreaseBrightnessFilter();
                break;
            case "shrooms":
                filter = new ShroomsFilter();
                break;
            default:
                throw new NotSupportedException();
        }

        return filter;
    }

    private static void ValidateInputPath(ref string s)
    {
        while (!File.Exists(s))
        {
            Console.Error.WriteLine("Input file does not exist. Reading from stdin");
            s = Console.ReadLine();
        }
    }
}
