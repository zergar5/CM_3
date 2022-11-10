using CM_3.Models;
using CM_3.Tools.Precondition.Cholesky;

namespace CM_3_Tests;

public class SLAESolverTest
{
    private SparseMatrix _sparseMatrix;
    private double[] _pr;

    [SetUp]
    public void Setup()
    {
        _sparseMatrix = new SparseMatrix
        {
            N = 5,
            IG = new[] { 0, 0, 0, 2, 5, 6 },
            JG = new[] { 0, 1, 0, 1, 2, 3 },
            DI = new[] { Math.Sqrt(2.0), Math.Sqrt(2.0), 1.0, 1.0, 1.0 },
            GG = new[] { Math.Sqrt(2.0)/2, Math.Sqrt(2.0)/2, Math.Sqrt(2.0)/2, Math.Sqrt(2.0)/2, 0.0, 1.0 },
        };
        _pr = new[] { 4.0, 4.0, 5.0, 6.0, 3.0 };
    }

    [Test]
    public void CalcYTest()
    {
        var actual = new[] { 2.8284271247461898, 2.8284271247461898, 1.0, 2.0, 1.0 };
        var expected = SLAESolver.CalcY(_sparseMatrix, _pr);
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestCase(new[] { 0.99999999999999989, 0.99999999999999989, 1.0, 1.0, 1.0 })]
    public void CalcXTest(double[] actual)
    {
        var y = new[] { 2.8284271247461898, 2.8284271247461898, 1.0, 2.0, 1.0 };
        var expected = SLAESolver.CalcX(_sparseMatrix, y);
        CollectionAssert.AreEquivalent(expected, actual);
    }
}