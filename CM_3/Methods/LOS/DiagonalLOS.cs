using CM_3.Models;
using CM_3.Tools;

namespace CM_3.Methods.LOS;

public class DiagonalLOS : LOS
{
    private double[] _m;

    public override void PrepareProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, out double[] r0, out double[] z0, out double[] p0)
    {
        _m = Preconditioner.CreateMatrix(sparseMatrix.DI);
        r0 = Calculator.MultiplyDiagonalOnVector(_m,
            Calculator.SubtractVectors(pr,
                Calculator.MultiplyMatrixOnVector(sparseMatrix, x)));
        z0 = Calculator.MultiplyDiagonalOnVector(_m, r0);
        p0 = Calculator.MultiplyDiagonalOnVector(_m, 
            Calculator.MultiplyMatrixOnVector(sparseMatrix, z0));
    }

    public override double[] IterationProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter, double[] r0, double[] z0, double[] p0)
    {
        Console.WriteLine("DiagonalLOS");
        var r = r0;
        var z = z0;
        var p = p0;
        var residual = Calculator.ScalarProduct(r, r);
        var residual0 = residual;
        var epsPow2 = eps * eps;
        for (var i = 1; i <= maxIter && residual > epsPow2; i++)
        {
            var scalarPP = Calculator.ScalarProduct(p, p);

            var alphaK = Calculator.ScalarProduct(p, r) / scalarPP;

            var xNext = Calculator.SumVectors(x,
                Calculator.MultiplyVectorOnNumber(z, alphaK));

            var rNext = Calculator.SubtractVectors(r,
                Calculator.MultiplyVectorOnNumber(p, alphaK));

            var LAURNext = Calculator.MultiplyDiagonalOnVector(_m, 
                Calculator.MultiplyMatrixOnVector(sparseMatrix, 
                    Calculator.MultiplyDiagonalOnVector(_m, rNext)));

            var betaK = -(Calculator.ScalarProduct(p, LAURNext) / scalarPP);

            var zNext = Calculator.SumVectors(
                Calculator.MultiplyDiagonalOnVector(_m, rNext),
                Calculator.MultiplyVectorOnNumber(z, betaK));

            var pNext = Calculator.SumVectors(LAURNext,
                Calculator.MultiplyVectorOnNumber(p, betaK));

            residual = Calculator.ScalarProduct(rNext, rNext) / residual0;

            x = xNext;
            r = rNext;
            z = zNext;
            p = pNext;

            CourseHolder.GetInfo(i, residual);
        }
        Console.WriteLine();
        return x;
    }
}