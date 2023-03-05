using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel;

public class BrightBordersFilter : ImageFilter
{
    private MedianFilter _median;
    private MaxFilter _max;
    private SobelFilter _sobel;

    public BrightBordersFilter()
    {
        _median = new MedianFilter();
        _max = new MaxFilter();
        _sobel = new SobelFilter();
    }

    public override string Name => "BrightBorders";
    public override Image<Argb32> Process(Image<Argb32> source)
    {
        return _max.Process(_sobel.Process(_median.Process(source)));
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        throw new NotImplementedException();
    }
}