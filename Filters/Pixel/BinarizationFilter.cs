using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class BinarizationFilter : ImageFilter
{
    public override string Name => "binary";
    private int _threshold;

    public BinarizationFilter(int threshold = 127)
    {
        _threshold = threshold;
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var intensity = (int)(0.36 * source[i, j].R + 0.53 * source[i, j].G + 0.11 * source[i, j].B);
        if (intensity < _threshold)
        {
            return new Argb32(0, 0, 0, 255);
        }
        else
        {
            return new Argb32(1f, 1f, 1f);
        }
    }
}