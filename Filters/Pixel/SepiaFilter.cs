using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class SepiaFilter : ImageFilter
{

    public override string Name => $"sepia_{sepiaCoeff}";
    private float sepiaCoeff;

    public SepiaFilter(float sepiaCoeff = 30f)
    {
        this.sepiaCoeff = sepiaCoeff;
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var pixel = source[i, j];
        var intensity = pixel.R * .36f + pixel.G * .53f + pixel.B * .11f;
        return new Argb32(
            (byte)Math.Clamp(intensity + 2 * this.sepiaCoeff, 0, 0xFF),
            (byte)Math.Clamp(intensity + .5f * this.sepiaCoeff, 0, 0xFF),
            (byte)Math.Clamp(intensity - this.sepiaCoeff, 0, 0xFF)
        );
    }
}
