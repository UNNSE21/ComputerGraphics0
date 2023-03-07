using System.Net.Sockets;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local.MathMorph;

public class GranientFilter : MathMorphFilter
{
    private DilationFilter _dilater;
    private Image<Argb32> _dilatedImage;
    private ErosionFilter _eroser;
    private Image<Argb32> _erosedImage;
    public GranientFilter(bool[,] structureElement, (int, int) structureElementAnchor) : base(structureElement, structureElementAnchor)
    {
        _dilater = new DilationFilter(structureElement, structureElementAnchor);
        _eroser = new ErosionFilter(structureElement, structureElementAnchor);
    }

    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _dilatedImage = _dilater.Process(source);
        _erosedImage = _eroser.Process(source);
        return base.Process(source);
    }

    public override string Name => "Gradient";
    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32(
            (byte) Math.Clamp(_dilatedImage[i, j].R - _erosedImage[i, j].R, 0, 255),
            (byte) Math.Clamp(_dilatedImage[i, j].G -_erosedImage[i, j].G, 0, 255),
            (byte) Math.Clamp(_dilatedImage[i, j].B - _erosedImage[i, j].B, 0, 255)
        );
    }
}