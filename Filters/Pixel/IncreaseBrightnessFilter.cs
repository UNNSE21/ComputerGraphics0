using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

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
        for (int i = 0; i < source.Width; i++)
        {
            for(int j = 0; j < source.Height; j++)
            {
                var pixel = source[i, j];

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(pixel.R + this.k, 0, 0xFF),
                    (byte)Math.Clamp(pixel.G + this.k, 0, 0xFF),
                    (byte)Math.Clamp(pixel.B + this.k, 0, 0xFF)
                );
            }
        }

        return source;
    }
}
