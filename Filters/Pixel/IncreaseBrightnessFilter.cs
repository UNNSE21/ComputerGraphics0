using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class IncreaseBrightnessFilter : ImageFilter
{
    public override string Name => $"increase_brightness_{k:+0;-#}";
    private readonly int k;

    public IncreaseBrightnessFilter(int k = 50)
    {
        this.k = k;
    }
    
    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var pixel = source[i, j];
        return new Argb32(
            (byte)Math.Clamp(pixel.R + this.k, 0, 0xFF),
            (byte)Math.Clamp(pixel.G + this.k, 0, 0xFF),
            (byte)Math.Clamp(pixel.B + this.k, 0, 0xFF)
        );
    }
}
