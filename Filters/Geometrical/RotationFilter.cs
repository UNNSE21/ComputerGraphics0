using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class RotationFilter : GeometricalFilter
    {
        public override string Name => $"Rotated_{_angle}";
        
        private float _angle;

        public RotationFilter(float angle)
        {
            _angle = angle;
        }
        
        
        protected override (int, int) CalculatePoint(int k, int l)
        {
            return (
                (int)((k - _center.Item1)*Math.Cos(_angle) - (l - _center.Item2)*Math.Sin(_angle) + _center.Item1),
                (int)((k - _center.Item1)*Math.Sin(_angle) + (l - _center.Item2)*Math.Cos(_angle) + _center.Item2)
            );
        }
    }
}