using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters;

public interface IImageFilter
{
    String Name { get; } 
    Image<Argb32> Process(Image<Argb32> source);
}