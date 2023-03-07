namespace ComputerGraphics0.Filters.Local;

public class SharpnessIncreaseFilter : ConvolutionFilter
{
    public override string Name => "Increased_Sharpness";

    public SharpnessIncreaseFilter()
    {
        _kernel = new[,]
        {
            {0f, -1f, 0f},
            {-1f, 5f, -1f},
            {0f, -1f, 0f}
        };
    }
}