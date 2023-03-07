using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class GlassFilter : GeometricalFilter
    {
        public override string Name => "Glass";
        protected override (int, int) CalculatePoint(int k, int l)
        {
            return (
                (int)(k + (Random.Shared.NextDouble()-0.5) * 10),
                (int)(l + (Random.Shared.NextDouble()-0.5) * 10)
            );
        }
    }
}