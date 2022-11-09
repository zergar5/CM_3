using CM_3.Models;
using CM_3.Tools.Precondition.Cholesky;

namespace CM_3.Tools.Precondition;

public static class Preconditioner
{
    public static double[] CreateMatrix(double[] di)
    {
        var n = di.Length;
        var result = new double[n];
        for (var i = 0; i < n; i++)
        {
            result[i] = Math.Sqrt(1.0 / di[i]);
        }

        return result;
    }

    public static SparseMatrix CreateMatrix(SparseMatrix sparseMatrix)
    {
        var matrix = IncompleteCholeskyDecomposition.Decomposition(sparseMatrix);
        return matrix;
    }
}