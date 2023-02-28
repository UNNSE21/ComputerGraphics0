using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerGraphics0.Filters;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.MathMorph
{
    public class Erosion : MathMorphFilter
    {
        public override string Name => "erosion";
        public Erosion(bool[,] structureElement, (int, int) strElPivot)
         : base(structureElement, strElPivot){
            
        }

        protected override Argb32 GetNewPixel(Image<Argb32> source, int x, int y)
        {
            Argb32 result = new Argb32();
            var minIntensity = 255;
            for(int i = 0; i < strEl.GetLength(0); ++i)
            {
                for(int j = 0; j < strEl.GetLength(0); ++j)
                {
                    if(strEl[i, j])
                    {
                        var pix = source[Math.Clamp(x + (i - strElPivot.Item1), 0, source.Width-1),
                            Math.Clamp(y + (j - strElPivot.Item2), 0, source.Height-1)];
                        var intensity = (int)(0.36 * pix.R + 0.53 * pix.G + 0.11 * pix.B);
                        if(intensity < minIntensity){ 
                            minIntensity = intensity;
                            result = pix;
                        }
                    }
                }
                
            }
            return result;
        }
    }
}