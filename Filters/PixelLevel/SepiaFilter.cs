using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.PixelLevel;

public class SepiaFilter : IImageFilter
{

    public string Name => "sepia";
    public float sepiaCoeff;

    public SepiaFilter(float sepiaCoeff = 30f)
    {
        this.sepiaCoeff = sepiaCoeff;
    }

    public Image<Argb32> Process(Image<Argb32> source)
    {
        Parallel.For(0, source.Width, (i) =>
        {
            Parallel.For(0, source.Height, (j) =>
            {
                var pixel = source[i, j];
                var intensity = pixel.R * .36f + pixel.G * .53f + pixel.B * .11f;

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(intensity + 2 * this.sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity + .5f * this.sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity - this.sepiaCoeff, 0, 0xFF)
                );
            });
        });

        return source;
    }
}
