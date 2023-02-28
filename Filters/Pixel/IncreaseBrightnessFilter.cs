using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.PixelLevel;

public class IncreaseBrightnessFilter : IImageFilter
{
    public string Name => "increase_brightness";
    private int k;

    public IncreaseBrightnessFilter(int k = 50)
    {
        this.k = k;
    }

    public Image<Argb32> Process(Image<Argb32> source)
    {
        Parallel.For(0, source.Width, (i) =>
        {
            Parallel.For(0, source.Height, (j) =>
            {
                var pixel = source[i, j];

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(pixel.R + this.k, 0, 0xFF),
                    (byte)Math.Clamp(pixel.G + this.k, 0, 0xFF),
                    (byte)Math.Clamp(pixel.B + this.k, 0, 0xFF)
                );
            });
        });

        return source;
    }
}
