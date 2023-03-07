using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class WavesHorizontalFilter : GeometricalFilter
    {
        public override string Name => "WavesHorizontal";
        protected override (int, int) CalculatePoint(int k, int l)
        {
            return (
                (int)(k+20*Math.Sin(Math.PI * 2 * l / 60)),
                l
            );
        }
    }
}