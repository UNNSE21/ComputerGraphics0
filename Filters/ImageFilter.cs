#define PARALLEL //Закомментировать, если нужно отключить параллельный for

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace ComputerGraphics0.Filters
{
    public abstract class ImageFilter : IImageFilter
    {
        public abstract string Name { get; }

        public virtual Image<Argb32> Process(Image<Argb32> source)
        {
            Image<Argb32> result = new Image<Argb32>(source.Width, source.Height);

#if PARALLEL
            Parallel.For(0, source.Width,
                i => { Parallel.For(0, source.Height, j => { result[i, j] = GetNewPixel(source, i, j); }); });
#else

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    result[i, j] = GetNewPixel(source, i, j);
                }
            }
#endif
            return result;
        }

        protected abstract Argb32 GetNewPixel(Image<Argb32> source, int i, int j);
    }
}