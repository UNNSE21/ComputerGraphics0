using ComputerGraphics0.Filters.Pixel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel.MathMorph;

public class OuterBorderFilter : MathMorphFilter
{
    private DilationFilter _dilater;
    private Image<Argb32> _dilatedImage;
    private BinarizationFilter _binarizator;

    public OuterBorderFilter(bool[,] structureElement, (int, int) structureElementAnchor, int threshold = 127) : base(structureElement, structureElementAnchor)
    {
        _dilater = new DilationFilter(structureElement, structureElementAnchor);
        _binarizator = new BinarizationFilter(threshold);
    }

    public override string Name => "outerBorder";
    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _dilatedImage = _binarizator.Process(_dilater.Process(source));
        return base.Process(_binarizator.Process(source));
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32(
            (byte) (_dilatedImage[i, j].R - source[i, j].R),
            (byte) (_dilatedImage[i, j].G - source[i, j].G),
            (byte) (_dilatedImage[i, j].B - source[i, j].B)
        );
    }
}