using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;

namespace ComputerGraphics0.Filters.Local;

public class MedianFilter : ImageFilter
{
    private int _radius;

    public MedianFilter(int radius = 1)
    {
        _radius = radius;
    }

    public override string Name => "Median";
    protected override Argb32 GetNewPixel(Image<Argb32> source, int x, int y)
    {
        int size = 2 * _radius + 1;
        byte[] pixelsR = new byte[size * size];
        byte[] pixelsG = new byte[size * size];
        byte[] pixelsB = new byte[size * size];
        
        byte resultR = 0, resultG = 0, resultB = 0;
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; j++)
            {
                var pix = source[
                    Math.Clamp(x + i - _radius, 0, source.Width- 1),
                    Math.Clamp(y + j - _radius, 0, source.Height - 1)];
                pixelsR[i * size + j] = pix.R;
                pixelsG[i * size + j] = pix.G;
                pixelsB[i * size + j] = pix.B;
            }
        }
        resultR = pixelsR.OrderBy(p => p).Skip(_radius).First();
        resultG = pixelsG.OrderBy(p => p).Skip(_radius).First();
        resultB = pixelsB.OrderBy(p => p).Skip(_radius).First();
        return new Argb32(resultR, resultG, resultB);
    }
}