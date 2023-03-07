using System.Net.Sockets;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local.MathMorph;

public class TopHatFilter : MathMorphFilter
{
    private OpeningFilter _opener;
    private Image<Argb32> _openedImage;
    public TopHatFilter(bool[,] structureElement, (int, int) structureElementAnchor) : base(structureElement, structureElementAnchor)
    {
        _opener = new OpeningFilter(structureElement, structureElementAnchor);
    }

    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _openedImage = _opener.Process(source);
        return base.Process(source);
    }

    public override string Name => "TopHat";
    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32(
            (byte) Math.Clamp(source[i, j].R - _openedImage[i, j].R, 0, 255),
            (byte) Math.Clamp(source[i, j].G -_openedImage[i, j].G, 0, 255),
            (byte) Math.Clamp(source[i, j].B - _openedImage[i, j].B, 0, 255)
        );
    }
}