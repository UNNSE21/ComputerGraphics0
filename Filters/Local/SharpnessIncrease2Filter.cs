namespace ComputerGraphics0.Filters.Kernel;

public class SharpnessIncrease2Filter : ConvolutionFilter
{
    public override string Name => "Increased_Sharpness";

    public SharpnessIncrease2Filter()
    {
        _kernel = new[,]
        {
            {-1f, -1f, -1f},
            {-1f, 9f, -1f},
            {-1f, -1f, -1f}
        };
    }
}
