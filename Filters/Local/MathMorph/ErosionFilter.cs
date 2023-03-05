using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Kernel.MathMorph
{
    public class ErosionFilter : MathMorphFilter
    {
        public override string Name => "erosion";
        public ErosionFilter(bool[,] structureElement, (int, int) structureElementAnchor)
         : base(structureElement, structureElementAnchor){
            
        }

        protected override Argb32 GetNewPixel(Image<Argb32> source, int x, int y)
        {
            Argb32 result = new Argb32();
            var minIntensity = 255;
            for(int i = 0; i < StructureElement.GetLength(0); ++i)
            {
                for(int j = 0; j < StructureElement.GetLength(0); ++j)
                {
                    if(StructureElement[i, j])
                    {
                        var pix = source[Math.Clamp(x + (i - StructureElementAnchor.Item1), 0, source.Width-1),
                            Math.Clamp(y + (j - StructureElementAnchor.Item2), 0, source.Height-1)];
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