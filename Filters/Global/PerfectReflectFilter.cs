using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Global;

public class PerfectReflectFilter : IImageFilter
{
    public string Name => "perfect_reflect";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        var (maxR, maxG, maxB) = (-1f, -1f, -1f);
        for (int i = 0; i < source.Width; i++)
        {
            for (int j = 0; j < source.Height; j++)
            {
                var pixel = source[i, j];
                maxR = Math.Max(maxR, pixel.R);
                maxG = Math.Max(maxG, pixel.G);
                maxB = Math.Max(maxB, pixel.B);
            }
        }

        for (int i = 0; i < source.Width; i++)
        {
            for (int j = 0; j < source.Height; j++)
            {
                var pixel = source[i, j];

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(pixel.R * 255 / maxR, 0, 0xFF),
                    (byte)Math.Clamp(pixel.G * 255 / maxG, 0, 0xFF),
                    (byte)Math.Clamp(pixel.B * 255 / maxB, 0, 0xFF)
                );
            }
        }

        return source;
    }
}
