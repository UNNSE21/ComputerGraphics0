using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;

namespace ComputerGraphics0.Filters.Local;

public class MaxFilter : ImageFilter
{
    private int _radius;

    public MaxFilter(int radius = 1)
    {
        _radius = radius;
    }

    public override string Name => "Max";
    protected override Argb32 GetNewPixel(Image<Argb32> source, int x, int y)
    {
        int size = 2 * _radius + 1;
        byte maxR = 0, maxG = 0, maxB = 0;
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; j++)
            {
                var pix = source[
                    Math.Clamp(x + i - _radius, 0, source.Width- 1),
                    Math.Clamp(y + j - _radius, 0, source.Height - 1)];
                if (pix.R > maxR)
                    maxR = pix.R;
                if (pix.G > maxG)
                    maxG = pix.G;
                if (pix.B > maxB)
                    maxB = pix.B;
            }
        }
        return new Argb32(maxR, maxG, maxB);
    }
}