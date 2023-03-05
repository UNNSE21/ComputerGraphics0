using ComputerGraphics0.Filters.Pixel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel.MathMorph;

public class InnerBorderFilter : MathMorphFilter
{
    private ErosionFilter _eroser;
    private Image<Argb32> _erosedImage;
    private BinarizationFilter _binarizator;

    public InnerBorderFilter(bool[,] structureElement, (int, int) structureElementAnchor, int threshold = 127) : base(structureElement, structureElementAnchor)
    {
        _eroser = new ErosionFilter(structureElement, structureElementAnchor);
        _binarizator = new BinarizationFilter(threshold);
    }

    public override string Name => "innerBorder";
    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _erosedImage = _binarizator.Process(_eroser.Process(source));
        return base.Process(_binarizator.Process(source));
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        return new Argb32(
            (byte) (source[i, j].R - _erosedImage[i, j].R),
            (byte) (source[i, j].G - _erosedImage[i, j].G),
            (byte) (source[i, j].B - _erosedImage[i, j].B)
        );
    }
}