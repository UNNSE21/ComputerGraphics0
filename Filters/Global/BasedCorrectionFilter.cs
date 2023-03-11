using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Global;

public class BasedCorrectionFilter : IImageFilter
{
    public string Name => "based_correction";
    private Argb32 color;
    public BasedCorrectionFilter(Argb32 color) {
        this.color = color;
    }
    public Image<Argb32> Process(Image<Argb32> source)
    {

        for (int i = 0; i < source.Width; i++)
        {
            for (int j = 0; j < source.Height; j++)
            {
                var pixel = source[i, j];

                source[i, j] = new Argb32(
                    (byte)Math.Clamp(
                        (float)pixel.R * (float)pixel.R / (float)this.color.R,
                        0,
                        0xFF
                    ),
                    (byte)Math.Clamp(
                        (float)pixel.G * (float)pixel.G / (float)this.color.G,
                        0,
                        0xFF
                    ),
                    (byte)Math.Clamp(
                        (float)pixel.B * (float)pixel.B / (float)this.color.B,
                        0,
                        0xFF
                    )
                );

            }
        }

        return source;
    }
}
