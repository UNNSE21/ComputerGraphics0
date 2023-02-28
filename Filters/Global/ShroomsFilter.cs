using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Global;

public class ShroomsFilter : IImageFilter
{
    public string Name => "shrooms";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        var (avgR, avgG, avgB) = (0f, 0f, 0f);
        var count = 0;

        for (int i = 0; i < source.Width; i++)
        {
            for (int j = 0; j < source.Height; j++)
            {
                count += 1;
                var pixel = source[i, j];
                avgR += (avgR + pixel.R) / count;
                avgG += (avgG + pixel.G) / count;
                avgB += (avgB + pixel.B) / count;
            }
        }

        var avg = (avgR + avgG + avgB) / 3;

        Parallel.For(0, source.Width, (i) =>
        {
            Parallel.For(0, source.Height, (j) =>
            {
                var pixel = source[i, j];

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(pixel.R * (avg / avgR), 0, 0xFF),
                    (byte)Math.Clamp(pixel.G * (avg / avgG), 0, 0xFF),
                    (byte)Math.Clamp(pixel.B * (avg / avgB), 0, 0xFF)
                );
            });
        });

        return source;
    }
}
