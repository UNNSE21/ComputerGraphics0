// See https://aka.ms/new-console-template for more information

using ComputerGraphics0.Filters;
using ComputerGraphics0.Filters.PixelLevel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;

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
        
        filter = GetFilterByName(filterName, args.Skip(3).ToArray());
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

    private static IImageFilter GetFilterByName(string name, params string[] filterArgs)
    {
        // Если надо параметры для алгоритма - берем массив filterArgs и парсим его параметры из строк в нужный формат 
        // с соответствующими проверками
        IImageFilter filter;
        switch (name)
        {
            case "inv":
                filter = new InversionFilter();
                break;
            case "gray":
                filter = new GrayscaleFilter();
                break;
            default:
                throw new NotSupportedException();
        }

        return filter;
    }

    private static void ValidateInputPath(ref string s)
    {
        while (String.IsNullOrEmpty(s) ||  s == "-" || !File.Exists(s))
        {
            Console.Error.WriteLine("Input file does not exist. Reading from stdin");
            s = Console.ReadLine();
        }
    }
}