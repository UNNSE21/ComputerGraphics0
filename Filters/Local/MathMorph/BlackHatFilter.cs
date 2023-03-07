using System.Net.Sockets;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local.MathMorph;

public class BlackHatFilter : MathMorphFilter
{
    private ClosingFilter _closer;
    private Image<Argb32> _closedImage;
    public BlackHatFilter(bool[,] structureElement, (int, int) structureElementAnchor) : base(structureElement, structureElementAnchor)
    {
        _closer = new ClosingFilter(structureElement, structureElementAnchor);
    }

    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _closedImage = _closer.Process(source);
        return base.Process(source);
    }

    public override string Name => "BlackHat";
    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32(
            (byte) Math.Clamp(_closedImage[i, j].R - source[i, j].R, 0, 255),
            (byte) Math.Clamp(_closedImage[i, j].G -source[i, j].G, 0, 255),
            (byte) Math.Clamp(_closedImage[i, j].B - source[i, j].B, 0, 255)
        );
    }
}