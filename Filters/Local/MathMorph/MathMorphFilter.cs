namespace ComputerGraphics0.Filters.Local.MathMorph
{
    public abstract class MathMorphFilter : ImageFilter
    {
        protected bool[,] StructureElement;
        protected (int, int) StructureElementAnchor;
        public MathMorphFilter(bool[,] structureElement, (int, int) structureElementAnchor)
        {
            StructureElement = structureElement;
            this.StructureElementAnchor = structureElementAnchor;
        }
    }
}