using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class GrayscaleFilter : IImageFilter
{
    public string Name => "grayscale";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        Parallel.For(0, source.Width, (i, state) =>
        {
            for (int j = 0; j < source.Height; j++)
            {
                var sourceVector = source[i, j].ToVector4();
                var intensity = sourceVector.X * .36f +
                    sourceVector.Y * .53f +
                    sourceVector.Z * .11f;
                source[i, j] = new Argb32(intensity, intensity, intensity);
            }
        });
        return source;
    }
}