using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Global;

public class LinearExpandFilter : ImageFilter
{
    public override string Name => "LinearExpansion";
    private int _maxR;
    private int _maxG;
    private int _maxB;
    private int _minR;
    private int _minG;
    private int _minB;
    
    public override Image<Argb32> Process(Image<Argb32> source)
    {
        _maxR = 0;
        _maxG = 0;
        _maxB = 0;
        _minR = 0xFF;
        _minG = 0xFF;
        _minB = 0xFF;
        for (int i = 0; i < source.Width; ++i)
        {
            for (int j = 0; j < source.Height; ++j)
            {
                if (source[i, j].R > _maxR)
                    _maxR = source[i, j].R;
                if (source[i, j].G > _maxG)
                    _maxG = source[i, j].G;
                if (source[i, j].B > _maxB)
                    _maxB = source[i, j].B;
                if (source[i, j].R < _minR)
                    _minR = source[i, j].R;
                if (source[i, j].G < _minG)
                    _minG = source[i, j].G;
                if (source[i, j].B < _minB)
                    _minB = source[i, j].B;
            }
        }
        return base.Process(source);
    }

    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        var res = new Argb32(
            (byte)((source[i, j].R - _minR) * 0xFF / (_maxR - _minR)),
            (byte)((source[i, j].G - _minG) * 0xFF / (_maxG - _minG)),
            (byte)((source[i, j].B - _minB) * 0xFF/ (_maxB - _minB))
        );
        return res;
    }
}