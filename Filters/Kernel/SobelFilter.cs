using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel;

public class SobelFilter : ImageFilter
{
    private readonly SobelXFilter _sobelX;
    private readonly SobelYFilter _sobelY;

    private Image<Argb32> _derivativeX;
    private Image<Argb32> _derivativeY;
    private class SobelXFilter : ConvolutionFilter
    {
        public override string Name => "sobelX";

        public SobelXFilter()
        {
            _kernel = new float[,]
            {
                {-1f, -2f, -1f},
                { 0f,  0f,  0f},
                { 1f,  2f,  1f}
            };
        }
    }
    
    private class SobelYFilter : ConvolutionFilter
    {
        public override string Name => "sobelY";

        public SobelYFilter()
        {
            _kernel = new float[,]
            {
                { 1f,  0f, -1f},
                { 2f,  0f, -2f},
                { 1f,  0f, -1f}
            };
        }
    }


    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _derivativeX = _sobelX.Process(source);
        _derivativeY = _sobelY.Process(source);
        return base.Process(source);
    }

    public SobelFilter()
    {
        _sobelX = new SobelXFilter();
        _sobelY = new SobelYFilter();
    }

    public override string Name => "Sobel";

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