using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel;

public class PriuttaFilter : ImageFilter
{
    private readonly PriuttaXFilter _priuttaX;
    private readonly PriuttaYFilter _priuttaY;

    private Image<Argb32> _derivativeX;
    private Image<Argb32> _derivativeY;
    private class PriuttaXFilter : ConvolutionFilter
    {
        public override string Name => "priuttaX";

        public PriuttaXFilter()
        {
            _kernel = new float[,]
            {
                { -1f, 0f, 1f},
                { -1f, 0f, 1f},
                { -1f, 0f, 1f}
            };
        }
    }

    private class PriuttaYFilter : ConvolutionFilter
    {
        public override string Name => "priuttaY";

        public PriuttaYFilter()
        {
            _kernel = new float[,]
            {
                { -1f, -1f, -1f},
                { 0f, 0f, 0f},
                { 1f, 1f, 1f}
            };
        }
    }


    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _derivativeX = _priuttaX.Process(source);
        _derivativeY = _priuttaX.Process(source);
        return base.Process(source);
    }

    public PriuttaFilter()
    {
        _priuttaX = new PriuttaXFilter();
        _priuttaY = new PriuttaYFilter();
    }

    public override string Name => "Priutta";

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var gradMagR = Math.Clamp(Math.Sqrt(_derivativeX[i, j].R * _derivativeX[i, j].R + _derivativeY[i, j].R * _derivativeY[i, j].R), 0f, 255f);
        var gradMagG = Math.Clamp(Math.Sqrt(_derivativeX[i, j].G * _derivativeX[i, j].G + _derivativeY[i, j].G * _derivativeY[i, j].G), 0f, 255f); 
        var gradMagB = Math.Clamp(Math.Sqrt(_derivativeX[i, j].B * _derivativeX[i, j].B + _derivativeY[i, j].B * _derivativeY[i, j].B), 0f, 255f);
        //var gradMagR = Math.Abs(_derivativeX[i, j].R) + Math.Abs(_derivativeY[i, j].R);
        //var gradMagG = Math.Abs(_derivativeX[i, j].G) + Math.Abs(_derivativeY[i, j].G);
        //var gradMagB = Math.Abs(_derivativeX[i, j].B) + Math.Abs(_derivativeY[i, j].B);
        return new Argb32((byte)gradMagR , (byte)gradMagG, (byte)gradMagB);
    }
}
