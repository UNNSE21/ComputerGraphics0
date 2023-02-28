using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Pixel;

public class InversionFilter : IImageFilter
{
    public string Name => "inversion";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        for(int i = 0; i < source.Width; ++i)
        {
            for (int j = 0; j < source.Height; j++)
            {
                var res = new Argb32((byte)(255 - source[i, j].R),
                    (byte)(255 - source[i, j].G),
                    (byte)(255 - source[i, j].B));
                source[i, j] = res;
                
            }
        }
        return source;
    }
}