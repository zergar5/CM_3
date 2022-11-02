using CM_3.Models;

namespace CM_3.Methods.MCG;

public interface IMethod
{
    public double[] Solve(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter);
}