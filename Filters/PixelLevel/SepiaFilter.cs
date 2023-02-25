using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.PixelLevel;

public class SepiaFilter : IImageFilter
{
    public string Name => "sepia";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        const float sepiaCoeff = 30f;

        Parallel.For(0, source.Width, (i) =>
        {
            Parallel.For(0, source.Height, (j) =>
            {
                var pixel = source[i, j];
                var intensity = pixel.R * .36f + pixel.G * .53f + pixel.B * .11f;

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(intensity + 2 * sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity + .5f * sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity - sepiaCoeff, 0, 0xFF)
                );
            });
        });

        return source;
    }
}
