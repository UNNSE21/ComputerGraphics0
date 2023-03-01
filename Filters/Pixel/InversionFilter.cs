using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class InversionFilter : ImageFilter
{
    public override string Name => "inversion";

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32((byte)(255 - source[i, j].R),
            (byte)(255 - source[i, j].G),
            (byte)(255 - source[i, j].B));
    }
}