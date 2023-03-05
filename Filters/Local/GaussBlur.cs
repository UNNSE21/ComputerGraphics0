namespace ComputerGraphics0.Filters.Kernel;

public class GaussBlur : ConvolutionFilter
{
    public override string Name => $"gauss_blur_{_radius}_{_sigma}";
    private int _radius;
    private float _sigma;

    public GaussBlur(int radius = 3, float sigma = 1)
    {
        _radius = radius;
        _sigma = sigma;
        int size = radius * 2 + 1;
        _kernel = new float[size, size];

        var sum = 0f;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var distX = (i - radius)*(i - radius);
                var distY = (j - radius)*(j - radius);
                var coeff = (float)(Math.Exp(-(distX + distY) / (2 * sigma * sigma)));
                _kernel[i, j] = coeff;
                sum += coeff;
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                _kernel[i, j] /= sum;
            }
        }
    }
}