using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGraphics0.Filters.Local
{
    public class MotionBlurFilter : ConvolutionFilter
    {
        public override string Name => $"MotionBlur_{_size}";
        private int _size = 3;

        public MotionBlurFilter(int size = 5)
        {
            _kernel = new float[size,size];
            for(int i = 0; i < size; ++i)
            {
                _kernel[i, i] = 1 / size;
            }
        }
    }
}