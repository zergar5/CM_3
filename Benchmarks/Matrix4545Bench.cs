using BenchmarkDotNet.Attributes;
using CM_3.IO;
using CM_3.Methods.LOS;
using CM_3.Methods.MCG;
using CM_3.Models;

namespace Benchmarks;

public class Matrix4545Bench
{
    private string _root = @"F:\Visual Studio\Projects\CM_3\CM_3\Input\5Point\";
    private SparseMatrix _sparseMatrix;
    private double[] _pr;
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
        var sources = new SourcesForMatrix
        {
            NFile = "kuslau4545.txt",
            IGFile = "ig4545.txt",
            JGFile = "jg4545.txt",
            GGFile = "gg4545.txt",
            DIFile = "di4545.txt"
        };

        var matrixI = new MatrixIO(_root);
        var vectorI = new VectorIO(_root);
        var parametersI = new ParametersIO(_root);

        _sparseMatrix = new SparseMatrix();
        matrixI.Read(_sparseMatrix, sources);

        var tuple = parametersI.ReadMethodParameters("kuslau4545.txt");
        _maxIter = tuple.Item1;
        _eps = tuple.Item2;

        _pr = vectorI.ReadDouble("pr4545.txt");
        _x1 = vectorI.ReadDouble("start4545.txt");
        _x2 = vectorI.ReadDouble("start4545.txt");
        _x3 = vectorI.ReadDouble("start4545.txt");
        _x4 = vectorI.ReadDouble("start4545.txt");

        _MCG = new MCG();
        _LOS = new StandartLOS();
        _diagonalLOS = new DiagonalLOS();
        _choleskyLOS = new CholeskyLOS();
    }

    [Benchmark]
    public void MCG4545()
    {
        _MCG.Solve(_sparseMatrix, _x1, _pr, _eps, _maxIter);
    }

    [Benchmark]
    public void LOS4545()
    {
        _LOS.Solve(_sparseMatrix, _x2, _pr, _eps, _maxIter);
    }

    [Benchmark]
    public void LOSDiagonal4545()
    {
        _diagonalLOS.Solve(_sparseMatrix, _x3, _pr, _eps, _maxIter);
    }

    [Benchmark]
    public void LOSCholesky4545()
    {
        _choleskyLOS.Solve(_sparseMatrix, _x4, _pr, _eps, _maxIter);
    }
}