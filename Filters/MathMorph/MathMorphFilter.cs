using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.MathMorph
{
    public abstract class MathMorphFilter : ImageFilter
    {
        protected bool[,] strEl;
        protected (int, int) strElPivot;
        public MathMorphFilter(bool[,] structureElement, (int, int) strElPivot)
        {
            strEl = structureElement;
            this.strElPivot = strElPivot;
        }
    }
}