using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Geometrical
{
    public abstract class GeometricalFilter : ImageFilter
    {
        protected (int, int) _center;

        public override Image<Argb32> Process(Image<Argb32> source)
        {
            _center = (source.Width / 2, source.Height / 2);
            return base.Process(source);
        }
        protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
        {
            var point = CalculatePoint(i, j);
            if(point.Item1 < 0 || point.Item1 >= source.Width 
                || point.Item2 < 0 || point.Item2 >= source.Height)
            {
                return new Argb32(0, 0, 0);
            }
            return source[point.Item1, point.Item2];
        }
        protected abstract (int, int) CalculatePoint(int k, int l);
    }
}