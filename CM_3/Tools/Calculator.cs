using CM_3.Models;

namespace CM_3.Tools;

public static class Calculator
{
    public static double[] MultiplyMatrixOnVector(SparseMatrix sparseMatrix, double[] pr)
    {
        var n = sparseMatrix.N;
        var ig = sparseMatrix.IG;
        var jg = sparseMatrix.JG;
        var gg = sparseMatrix.GG;
        var di = sparseMatrix.DI;
        var result = new double[n];

        for (var i = 0; i < n; i++)
        {
            result[i] += di[i] * pr[i];
            for (var j = ig[i]; j < ig[i + 1]; j++)
            {
                result[i] += gg[j] * pr[jg[j]];
                result[jg[j]] += gg[j] * pr[i];
            }
        }

        return result;
    }

    public static double[] MultiplyVectorOnNumber(double[] vector, double number)
    {
        var n = vector.Length;
        var result = new double[n];

        for (var i = 0; i < n; i++)
        {
            result[i] = vector[i] * number;
        }

        return result;
    }

    public static double[] SubtractVectors(double[] vectorA, double[] vectorB)
    {
        var n = vectorA.Length;
        var result = new double[n];
        for (var i = 0; i < n; i++)
        {
            result[i] = vectorA[i] - vectorB[i];
        }

        return result;
    }

    public static double[] SumVectors(double[] vectorA, double[] vectorB)
    {
        var n = vectorA.Length;
        var result = new double[n];
        for (var i = 0; i < n; i++)
        {
            result[i] = vectorA[i] + vectorB[i];
        }

        return result;
    }

    public static double ScalarProduct(double[] vectorA, double[] vectorB)
    {
        var n = vectorA.Length;
        var result = 0.0;
        for (var i = 0; i < n; i++)
        {
            result += vectorA[i] * vectorB[i];
        }

        return result;
    }

    public static double CalcNorm(double[] vector)
    {
        var n = vector.Length;
        var result = 0.0;

        for (var i = 0; i < n; i++)
        {
            result += vector[i] * vector[i];
        }

        return Math.Sqrt(result);
    }

    public static double[] MultiplyDiagonalOnVector(double[] diagonal, double[] vector)
    {
        var n = diagonal.Length;
        var result = new double[n];
        for (var i = 0; i < n; i++)
        {
            result[i] = diagonal[i] * vector[i];
        }

        return result;
    }
}