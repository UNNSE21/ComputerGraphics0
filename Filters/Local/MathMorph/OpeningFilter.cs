using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ComputerGraphics0.Filters.Local.MathMorph;

public class OpeningFilter : MathMorphFilter
{
    private DilationFilter _dilater;
    private ErosionFilter _eroser;
    public OpeningFilter(bool[,] structureElement, (int, int) structureElementAnchor) : base(structureElement, structureElementAnchor)
    {
        _dilater = new DilationFilter(structureElement, structureElementAnchor);
        _eroser = new ErosionFilter(structureElement, structureElementAnchor);
    }

    public override string Name => "opening";
    public override Image<Argb32> Process(Image<Argb32> source)
    {
        return _dilater.Process(_eroser.Process(source));
    }

    // TODO: убрать этот мусор, не добавляя дублированный код. Хз как решить без множественного наследования или костыля в виде стандартной реализации. Функция не публичная, так что не так критично
    protected override Argb32 GetNewPixel(Image<Argb32> source, int i, int j)
    {
        throw new NotImplementedException();
    }
}