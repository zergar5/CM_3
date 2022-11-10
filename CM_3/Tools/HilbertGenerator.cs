using CM_3.Models;

namespace CM_3.Tools;

public class HilbertGenerator
{
    public SparseMatrix Generate(int size)
    {
        var n = size;
        var ig = new int[n + 1];
        var jg = new int[(1 + n - 1) * (n - 1) / 2];
        var gg = new double[(1 + n - 1) * (n - 1) / 2];
        var di = new double[n];

        var k = 0;
        ig[0] = 0;
        for (var i = 0; i < n; i++)
        {
            di[i] = 1.0 / ((i + 1.0) + (i + 1.0) - 1.0);
            for (var j = 0; j < i; j++, k++)
            {
                gg[k] = 1.0 / ((j + 1.0) + (i + 1.0) - 1.0);
                jg[k] = j;
            }
            ig[i + 1] = k;
        }

        var sparseMatrix = new SparseMatrix
        {
            N = n,
            IG = ig,
            JG = jg,
            GG = gg,
            DI = di
        };
        return sparseMatrix;
    }

    public double[] GenerateXStar(long size)
    {
        var xStar = new double[size];
        for (long i = 0; i < size; i++)
        {
            xStar[i] = i + 1.0;
        }
        return xStar;
    }

    public double[] GenerateXStart(long size)
    {
        var xStar = new double[size];
        for (long i = 0; i < size; i++)
        {
            xStar[i] = 0.0;
        }
        return xStar;
    }
}