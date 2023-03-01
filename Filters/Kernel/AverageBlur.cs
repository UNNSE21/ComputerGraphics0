namespace ComputerGraphics0.Filters.Kernel;

public class AverageBlur : KernelFilter
{
    public override string Name => $"blur_avg_{_radius}";
    private int _radius;

    public AverageBlur(int radius = 10)
    {
        _radius = radius;
        var coeff = 1f/((radius * 2 + 1) * (radius * 2 + 1));
        _kernel = new float[radius * 2 + 1, radius * 2 + 1];
        for (int i = 0; i < _kernel.GetLength(0); i++)
        {
            for (int j = 0; j < _kernel.GetLength(1); j++)
            {
                _kernel[i, j] = coeff;
            }
        }
    }
}