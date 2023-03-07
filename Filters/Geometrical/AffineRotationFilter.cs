using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerGraphics0.Filters.Geometrical.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class AffineRotationFilter : ImageFilter
    {
        public override string Name => $"Rotated(m)_{_angle}";
        private float _angle;
        private AffineTransform _transform;
        public AffineRotationFilter(float angle)
        {
            _angle = angle;
            float[,] matrix = AffineTransform.CalculateInverseMatrix(new float[3,3] {
                {(float) Math.Cos(_angle), (float) -Math.Sin(_angle), 0f},
                {(float) Math.Sin(_angle), (float) Math.Cos(_angle), 0f},
                {0f, 0f, 1f}
            });
            _transform = new AffineTransform(matrix);
        }
        protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
        {
            var point = _transform.Apply(i - source.Width/2, j - source.Height / 2);
            point.Item1 += source.Width / 2;
            point.Item2 += source.Height / 2;
            if(point.Item1 < 0 || point.Item1 >= source.Width 
                || point.Item2 < 0 || point.Item2 >= source.Height)
            {
                return new Argb32(0, 0, 0);
            }
            return source[point.Item1, point.Item2];
        }
    }
}