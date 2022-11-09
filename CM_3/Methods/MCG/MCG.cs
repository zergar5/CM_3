using CM_3.Models;
using CM_3.Tools;
using CM_3.Tools.SolutionCheck;

namespace CM_3.Methods.MCG;

public class MCG : IMethod
{
    private void PrepareProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, out double[] r0, out double[] z0)
    {
        var AxPr = Calculator.MultiplyMatrixOnVector(sparseMatrix, x);
        r0 = Calculator.SubtractVectors(pr, AxPr);
        z0 = r0;
    }

    public double[] Solve(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter)
    {
        PrepareProcess(sparseMatrix, x, pr, out var r0, out var z0);
        x = IterationProcess(sparseMatrix, x, pr, eps, maxIter, r0, z0);
        return x;
    }

    private double[] IterationProcess(SparseMatrix sparseMatrix, double[] x, double[] pr, double eps, int maxIter,
        double[] r0, double[] z0)
    {
        Console.WriteLine("MCG");
        var r = r0;
        var z = z0;
        var prNorm = Calculator.CalcNorm(pr);
        var residual = Calculator.CalcNorm(r) / prNorm;
        for (var i = 1; i <= maxIter && residual > eps; i++)
        {
            var scalarRR = Calculator.ScalarProduct(r, r);

            var AxZ = Calculator.MultiplyMatrixOnVector(sparseMatrix, z);

            var alphaK = scalarRR / Calculator.ScalarProduct(AxZ, z);

            var xNext = Calculator.SumVectors(x,
                Calculator.MultiplyVectorOnNumber(z, alphaK));

            var rNext = Calculator.SubtractVectors(r,

                Calculator.MultiplyVectorOnNumber(AxZ, alphaK));

            var betaK = Calculator.ScalarProduct(rNext, rNext) / scalarRR;

            var zNext = Calculator.SumVectors(rNext, Calculator.MultiplyVectorOnNumber(z, betaK));

            residual = Calculator.CalcNorm(rNext) / prNorm;

            x = xNext;
            r = rNext;
            z = zNext;

            CourseHolder.GetInfo(i, residual);
        }
        Console.WriteLine();
        return x;
    }
}