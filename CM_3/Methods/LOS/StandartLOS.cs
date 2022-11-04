using CM_3.Models;
using CM_3.Tools;

namespace CM_3.Methods.LOS;

public class StandartLOS : LOS
{
    public override void PrepareProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, out double[] r0, out double[] z0, out double[] p0)
    {
        r0 = Calculator.SubtractVectors(pr,
            Calculator.MultiplyMatrixOnVector(sparseMatrix, x));
        z0 = r0;
        p0 = Calculator.MultiplyMatrixOnVector(sparseMatrix, z0);
    }

    public override double[] IterationProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter,
        double[] r0, double[] z0, double[] p0)
    {
        Console.WriteLine("LOS");
        var r = r0;
        var z = z0;
        var p = p0;
        var residual = Calculator.ScalarProduct(r, r);
        for (var i = 1; i <= maxIter && residual >= eps; i++)
        {
            var scalarPP = Calculator.ScalarProduct(p, p);

            var alphaK = Calculator.ScalarProduct(p, r) / scalarPP;

            var xNext = Calculator.SumVectors(x, 
                Calculator.MultiplyVectorOnNumber(z, alphaK));

            var rNext = Calculator.SubtractVectors(r, 
                Calculator.MultiplyVectorOnNumber(p, alphaK));

            var AxRNext = Calculator.MultiplyMatrixOnVector(sparseMatrix, rNext);

            var betaK = -(Calculator.ScalarProduct(p, AxRNext) / scalarPP);

            var zNext = Calculator.SumVectors(rNext, 
                Calculator.MultiplyVectorOnNumber(z, betaK));

            var pNext = Calculator.SumVectors(AxRNext, 
                Calculator.MultiplyVectorOnNumber(p, betaK));

            residual = Calculator.ScalarProduct(rNext, rNext);

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