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
            var sumL = 0.0;
            for (var j = ig[i]; j < ig[i + 1]; j++)
            {
                sumL += gg[j] * y[jg[j]];
            }
            y[i] = (pr[i] - sumL) / di[i];
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
        for (var i = n - 1; i >= 0; i--)
        {
            var sumL = 0.0;
            for (var j = ig[i + 1]; j < ig[n]; j++)
            {
                sumL += gg[j] * y[jg[j]];
            }
            x[i] = (y[i] - sumL) / di[i];
        }

        return x;
    }
}