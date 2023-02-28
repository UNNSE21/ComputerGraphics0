using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class SepiaFilter : IImageFilter
{

    public string Name => "sepia";
    private float sepiaCoeff;

    public SepiaFilter(float sepiaCoeff = 30f)
    {
        this.sepiaCoeff = sepiaCoeff;
    }

    public Image<Argb32> Process(Image<Argb32> source)
    {
        for(int i = 0; i < source.Width; ++i)
        {
            for(int j = 0; j < source.Height; ++j)
            {
                var pixel = source[i, j];
                var intensity = pixel.R * .36f + pixel.G * .53f + pixel.B * .11f;

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(intensity + 2 * this.sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity + .5f * this.sepiaCoeff, 0, 0xFF),
                    (byte)Math.Clamp(intensity - this.sepiaCoeff, 0, 0xFF)
                );
            }
        }

        return source;
    }
}
