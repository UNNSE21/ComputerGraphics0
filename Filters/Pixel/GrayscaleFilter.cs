using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class GrayscaleFilter : ImageFilter
{
    public override string Name => "grayscale";

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var sourceVector = source[i, j].ToVector4();
        var intensity = sourceVector.X * .36f +
                        sourceVector.Y * .53f +
                        sourceVector.Z * .11f;
        return new Argb32(intensity, intensity, intensity);
    }
}