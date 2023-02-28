﻿// See https://aka.ms/new-console-template for more information

using ComputerGraphics0.Filters;
using ComputerGraphics0.Filters.Global;
using ComputerGraphics0.Filters.Pixel;
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
        // Если надо параметры для алгоритма - берем массив filterArgs и парсим его параметры из строк в нужный формат 
        // Исключения не ловим, поскольку их надо поймать снаружи и выйти из программы
        IImageFilter filter;
        switch (name)
        {
            case "inv":
                filter = new InversionFilter();
                break;
            case "gray":
                filter = new GrayscaleFilter();
                break;
//          case "parametrized":
//              filter = new ParamFilter(
//                  Int32.Parse(args[0],
//                  Int32.Parse(args[1])
//              );
//              break;
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
