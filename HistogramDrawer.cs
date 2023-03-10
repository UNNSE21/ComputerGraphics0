using ComputerGraphics0.Filters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;
using SixLabors.ImageSharp.Processing;

namespace ComputerGraphics0;

public class HistogramDrawer : IImageFilter
{
    public string Name => "histogram";
    public Image<Argb32> Process(Image<Argb32> source)
    {
        int[] histR = new int[256];
        int[] histG = new int[256];
        int[] histB = new int[256];
        for (int i = 0; i < source.Width; i++)
        {
            for (int j = 0; j < source.Height; j++)
            {
                histR[source[i, j].R]++;
                histG[source[i, j].G]++;
                histB[source[i, j].B]++;
            }
        }

        int maxR = histR.Max();
        int maxG= histG.Max();
        int maxB = histB.Max();

        Image<Argb32> result = new Image<Argb32>(2560, maxR + maxG + maxB);
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < histR[i]; j++)
            {
                for (int k = 0; k < 10; k++)
                    result[i*10+k, result.Height - j - 1] = new Argb32(255, 0, 0);
            }
            
            for (int j = 0; j < histG[i]; j++)
            {
                for (int k = 0; k < 10; k++)
                    result[i*10+k, result.Height - j - maxR - 1] = new Argb32(0, 255, 0);
            }
            
            for (int j = 0; j < histB[i]; j++)
            {
                for (int k = 0; k < 10; k++)
                    result[i*10+k, result.Height - j - maxR - maxG - 1] = new Argb32(0, 0, 255);
            }
        }
        result.Mutate(x=>x.Resize(result.Width, 2560));
        return result;
    }
}