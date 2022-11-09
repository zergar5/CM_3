using BenchmarkDotNet.Attributes;
using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;
using CM_3.Tools;

namespace Benchmarks;

public class Hilbert20Bench
{
    private string _root = @"F:\Visual Studio\Projects\CM_3\CM_3\Input\4Point\";
    private SparseMatrix _sparseMatrix20;
    private double[] _pr20;
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

        _sparseMatrix20 = new SparseMatrix();
        _sparseMatrix20.GenerateHilbert(matrixI, hilbertGenerator, "kuslau20.txt");

        var tuple = parametersI.ReadMethodParameters("kuslau20.txt");
        _maxIter = tuple.Item1;
        _eps = tuple.Item2;

        var xStar = hilbertGenerator.GenerateXStar(_sparseMatrix20.N);
        _x1 = hilbertGenerator.GenerateXStart(_sparseMatrix20.N);
        _x2 = hilbertGenerator.GenerateXStart(_sparseMatrix20.N);
        _x3 = hilbertGenerator.GenerateXStart(_sparseMatrix20.N);
        _x4 = hilbertGenerator.GenerateXStart(_sparseMatrix20.N);
        _pr20 = Calculator.MultiplyMatrixOnVector(_sparseMatrix20, xStar);

        _MCG = new MCG();
        _LOS = new StandartLOS();
        _diagonalLOS = new DiagonalLOS();
        _choleskyLOS = new CholeskyLOS();
    }

    [Benchmark]
    public void Hilbert20MCG()
    {
        _MCG.Solve(_sparseMatrix20, _x1, _pr20, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert20LOS()
    {
        _LOS.Solve(_sparseMatrix20, _x2, _pr20, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert20LOSDiagonal()
    {
        _diagonalLOS.Solve(_sparseMatrix20, _x3, _pr20, _eps, _maxIter);
    }

    [Benchmark]
    public void Hilbert20LOSCholesky()
    {
        _choleskyLOS.Solve(_sparseMatrix20, _x4, _pr20, _eps, _maxIter);
    }
}