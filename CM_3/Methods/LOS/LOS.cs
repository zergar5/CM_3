using CM_3.Methods.MCG;
using CM_3.Models;
using CM_3.Tools;

namespace CM_3.Methods.LOS;

public abstract class LOS : IMethod
{
    public abstract void PrepareProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, out double[] r0, out double[] z0,
        out double[] p0);

    public virtual double[] Solve(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter)
    {
        PrepareProcess(sparseMatrix, x, pr, out var r0, out var z0, out var p0);
        x = IterationProcess(sparseMatrix, x, pr, eps, maxIter, r0, z0, p0);
        return x;
    }

    public abstract double[] IterationProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter,
        double[] r0, double[] z0, double[] p0);
}