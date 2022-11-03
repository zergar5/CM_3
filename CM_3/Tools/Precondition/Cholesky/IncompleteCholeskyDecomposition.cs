using CM_3.Models;

namespace CM_3.Tools;

public class IncompleteCholeskyDecomposition
{
    public static SparseMatrix Decomposition(SparseMatrix sparseMatrix)
    {
        var n = sparseMatrix.N;
        var cIG = new int[n + 1];
        Array.Copy(sparseMatrix.IG, cIG, n + 1);
        var cJG = new int[cIG[n]];
        Array.Copy(sparseMatrix.JG, cJG, cIG[n]);
        var cGG = new double[cIG[n]];
        Array.Copy(sparseMatrix.GG, cGG, cIG[n]);
        var cDI = new double[n];
        Array.Copy(sparseMatrix.DI, cDI, n);

        for (var i = 0; i < n; i++)
        {
            var sumD = 0.0;
            for (var j = cIG[i]; j < cIG[i + 1]; j++)
            {
                var sumIPrev = 0.0;
                for (var k = cIG[i]; k < j; k++)
                {
                    var iPrev = i - cJG[j];
                    if (iPrev == i) continue;
                    var kPrev = Array.IndexOf(cJG, cJG[k], cIG[i - iPrev], cIG[i - iPrev + 1] - cIG[i - iPrev]);
                    if (kPrev != -1)
                    {
                        sumIPrev += cGG[k] * cGG[kPrev];
                    }
                }
                cGG[j] = (cGG[j] - sumIPrev) / cDI[cJG[j]];
                sumD += cGG[j] * cGG[j];
            }
            cDI[i] = Math.Sqrt(cDI[i] - sumD);
        }

        var choleskySparseMatrix = new SparseMatrix
        {
            N = n,
            IG = cIG,
            JG = cJG,
            GG = cGG,
            DI = cDI
        };

        return choleskySparseMatrix;
    }
}