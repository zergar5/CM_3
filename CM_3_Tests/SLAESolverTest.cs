using CM_3.Models;
using CM_3.Tools;

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
            IG = new[] { 0, 0, 0, 2, 4, 6 },
            JG = new[] { 0, 1, 1, 2, 0, 2 },
            DI = new[] { 2.0, 2.0, 2.0, 2.0, 2.0 },
            GG = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
        };
        _pr = new[] { 4.0, 4.0, 6.0, 4.0, 4.0 };
    }

    [Test]
    public void CalcYTest()
    {
        var actual = new[] { 2.0 * Math.Sqrt(2.0), 2.0 * Math.Sqrt(2.0), 2.0 / 5.0 * Math.Sqrt(5.0), Math.Sqrt(30.0) / 5 };
        var expected = SLAESolver.CalcY(_sparseMatrix, _pr);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestCase(new[] { 5.0, 5.0, 5.0, 5.0, 5.0 })]
    public void CalcXTest(double[] actual)
    {
        var expected = Calculator.MultiplyVectorOnNumber(_pr, 5.0);
        CollectionAssert.AreEqual(expected, actual);
    }
}