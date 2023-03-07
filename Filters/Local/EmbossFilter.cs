using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ComputerGraphics0.Filters.Pixel;

namespace ComputerGraphics0.Filters.Local;

public class EmbossFilter : ConvolutionFilter
{
    public override string Name => "emboss";

    private IncreaseBrightnessFilter _brighten;

    public EmbossFilter()
    {
        _kernel = new[,]
        {
            {0f, 1f, 0f},
            {1f, 0f, -1f},
            {0f, -1f, 0f}
        };

        _brighten = new IncreaseBrightnessFilter();
    }

    public override Image<Argb32> Process(Image<Argb32> source)
    {
        return _brighten.Process(base.Process(source));
    }
}
