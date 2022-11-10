using BenchmarkDotNet.Attributes;
using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;
using CM_3.Tools;

namespace Benchmarks;

public class Hilbert4Bench
{
    private string _root = @"F:\Visual Studio\Projects\CM_3\CM_3\Input\4Point\";
    private SparseMatrix _sparseMatrix4;
    private double[] _pr4;
    private double[] _x1;
    private double[] _x2;
    private double[] _x3;
    private double[] _x4;
    private double _eps;
    private int _maxIter;
    private MCG _MCG;
    private StandartLOS _LOS;
    private DiagonalLOS _diagonalLOS;
    private CholeskyLOS _choleskyLOS;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var matrixI = new MatrixIO(_root);
        var parametersI = new ParametersIO(_root);

        var hilbertGenerator = new HilbertGenerator();

        _sparseMatrix4 = new SparseMatrix();
        _sparseMatrix4.GenerateHilbert(matrixI, hilbertGenerator, "kuslau4.txt");

        var tuple = parametersI.ReadMethodParameters("kuslau4.txt");
        _maxIter = tuple.Item1;
        _eps = tuple.Item2;

        var xStar = hilbertGenerator.GenerateXStar(_sparseMatrix4.N);
        _x1 = hilbertGenerator.GenerateXStart(_sparseMatrix4.N);
        _x2 = hilbertGenerator.GenerateXStart(_sparseMatrix4.N);
        _x3 = hilbertGenerator.GenerateXStart(_sparseMatrix4.N);
        _x4 = hilbertGenerator.GenerateXStart(_sparseMatrix4.N);
        _pr4 = Calculator.MultiplyMatrixOnVector(_sparseMatrix4, xStar);

        _MCG = new MCG();
        _LOS = new StandartLOS();
        _diagonalLOS = new DiagonalLOS();
        _choleskyLOS = new CholeskyLOS();
    }

    [Benchmark]
    public void Hilbert4MCG()
    {
        _MCG.Solve(_sparseMatrix4, _x1, _pr4, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert4LOS()
    {
        _LOS.Solve(_sparseMatrix4, _x2, _pr4, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert4LOSDiagonal()
    {
        _diagonalLOS.Solve(_sparseMatrix4, _x3, _pr4, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert4LOSCholesky()
    {
        _choleskyLOS.Solve(_sparseMatrix4, _x4, _pr4, _eps, _maxIter);
    }
}