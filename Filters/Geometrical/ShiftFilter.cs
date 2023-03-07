using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Geometrical
{
    public class ShiftFilter : GeometricalFilter
    {
        public override string Name => $"shifted_{_shiftX}_{_shiftY}";
        private int _shiftX, _shiftY;
        public ShiftFilter(int shiftX = 50, int shiftY = 0)
        {
            _shiftX = shiftX;
            _shiftY = shiftY;
        }
        protected override (int, int) CalculatePoint(int k, int l)
        {
            return (
                k + _shiftX,
                l + _shiftY
            );
        }
    }
}