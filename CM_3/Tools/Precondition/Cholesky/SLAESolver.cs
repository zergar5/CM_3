using CM_3.Models;

namespace CM_3.Tools;

public static class SLAESolver
{
    public static double[] CalcY(SparseMatrix sparseMatrix, double[] pr)
    {
        var n = sparseMatrix.N;
        var ig = sparseMatrix.IG;
        var jg = sparseMatrix.JG;
        var gg = sparseMatrix.GG;
        var di = sparseMatrix.DI;
        var y = new double[n];
        for (var i = 0; i < n; i++)
        {
            var sum = 0.0;
            for (var j = ig[i]; j < ig[i + 1]; j++)
            {
                sum += gg[j] * y[jg[j]];
            }
            y[i] = (pr[i] - sum) / di[i];
        }

        return y;
    }

    public static double[] CalcX(SparseMatrix sparseMatrix, double[] y)
    {
        var n = sparseMatrix.N;
        var ig = sparseMatrix.IG;
        var jg = sparseMatrix.JG;
        var gg = sparseMatrix.GG;
        var di = sparseMatrix.DI;
        var x = new double[n];
        Array.Copy(y, x, n);
        for (var i = n - 1; i >= 0; i--)
        {
            x[i] /= di[i];
            for (var j = ig[i + 1] - 1; j >= ig[i]; j--)
            {
                x[jg[j]] -= gg[j] * x[i];
            }
        }

        return x;
    }
}