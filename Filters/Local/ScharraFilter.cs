using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local;

public class ScharraFilter : ImageFilter
{
    private readonly ScharraXFilter _scharraX;
    private readonly ScharraYFilter _scharraY;

    private Image<Argb32> _derivativeX;
    private Image<Argb32> _derivativeY;
    private class ScharraXFilter : ConvolutionFilter
    {
        public override string Name => "scharraX";

        public ScharraXFilter()
        {
            _kernel = new float[,]
            {
                { 3f, 0f,  -3f},
                { 10f, 0f, -10f},
                { 3f, 0f,  -3f}
            };
        }
    }

    private class ScharraYFilter : ConvolutionFilter
    {
        public override string Name => "scharraY";

        public ScharraYFilter()
        {
            _kernel = new float[,]
            {
                { 3f, 10f,  3f},
                { 0f, 0f, 0f},
                { -3f, -10f,  -3f}
            };
        }
    }


    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _derivativeX = _scharraX.Process(source);
        _derivativeY = _scharraX.Process(source);
        return base.Process(source);
    }

    public ScharraFilter()
    {
        _scharraX = new ScharraXFilter();
        _scharraY = new ScharraYFilter();
    }

    public override string Name => "Scharra";

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
