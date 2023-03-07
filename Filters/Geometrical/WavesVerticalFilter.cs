using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class WavesVerticalFilter : GeometricalFilter
    {
        public override string Name => "WavesVertical";
        protected override (int, int) CalculatePoint(int k, int l)
        {
            return (
                (int)(k+20*Math.Sin(Math.PI * 2 * k / 30)),
                l
            );
        }
    }
}