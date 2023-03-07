using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local;

public abstract class ConvolutionFilter : ImageFilter
{
    protected float[,] _kernel;

    protected override Argb32 GetNewPixel(Image<Argb32> source, int x, int y)
    {
        float resultR = 0.0f, resultG = 0.0f, resultB = 0.0f;
        int radiusX = _kernel.GetLength(0) / 2;
        int radiusY = _kernel.GetLength(1) / 2;
        for (int i = 0; i < _kernel.GetLength(0); ++i)
        {
            for (int j = 0; j < _kernel.GetLength(1); j++)
            {
                var pix = source[
                    Math.Clamp(x + i - radiusX, 0, source.Width- 1),
                    Math.Clamp(y + j - radiusY, 0, source.Height - 1)];
                resultR += _kernel[i, j] * pix.R;
                resultG += _kernel[i, j] * pix.G;
                resultB += _kernel[i, j] * pix.B;
            }
        }

        return new Argb32((byte)(Math.Clamp(resultR, 0, 255)), (byte)Math.Clamp(resultG, 0, 255), (byte)Math.Clamp(resultB, 0, 255));
    }
}