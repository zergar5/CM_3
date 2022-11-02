using CM_3.Models;
using CM_3.Tools;

namespace CM_3.Methods.LOS;

public class CholeskyLOS : LOS
{
    private SparseMatrix _m;
    public override void PrepareProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, out double[] r0, out double[] z0, out double[] p0)
    {
        _m = Preconditioner.CreateMatrix(sparseMatrix);
        r0 = SLAESolver.CalcY(_m,
            Calculator.SubtractVectors(pr,
                Calculator.MultiplyMatrixOnVector(sparseMatrix, x)));
        z0 = SLAESolver.CalcX(_m, r0);
        p0 = SLAESolver.CalcY(_m,
            Calculator.MultiplyMatrixOnVector(sparseMatrix, z0));
    }

    public override double[] IterationProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter, double[] r0, double[] z0, double[] p0)
    {
        var r = r0;
        var z = z0;
        var p = p0;
        var residual = Calculator.ScalarProduct(r, r);
        for (var i = 1; i <= maxIter && residual > eps; i++)
        {
            var scalarPP = Calculator.ScalarProduct(p, p);

            var alphaK = Calculator.ScalarProduct(p, r) / scalarPP;

            var xNext = Calculator.SumVectors(x,
                Calculator.MultiplyVectorOnNumber(z, alphaK));

            var rNext = Calculator.SubtractVectors(r,
                Calculator.MultiplyVectorOnNumber(p, alphaK));

            var LAURNext = SLAESolver.CalcY(_m,
                Calculator.MultiplyMatrixOnVector(sparseMatrix,
                    SLAESolver.CalcX(_m, rNext)));

            var betaK = -(Calculator.ScalarProduct(p, LAURNext) / scalarPP);

            var zNext = Calculator.SumVectors(
                SLAESolver.CalcX(_m, rNext),
                Calculator.MultiplyVectorOnNumber(z, betaK));

            var pNext = Calculator.SumVectors(
                Calculator.MultiplyDiagonalOnVector(LAURNext, rNext),
                Calculator.MultiplyVectorOnNumber(p, betaK));

            x = xNext;
            r = rNext;
            z = zNext;
            p = pNext;

            residual -= alphaK * alphaK * scalarPP;

            CourseHolder.GetInfo(i, residual);
        }

        Console.WriteLine();
        return x;
    }
}