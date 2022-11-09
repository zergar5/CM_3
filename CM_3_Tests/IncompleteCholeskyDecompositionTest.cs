using CM_3.Models;
using CM_3.Tools;
using CM_3.Tools.Precondition.Cholesky;

namespace CM_3_Tests;

public class IncompleteCholeskyDecompositionTest
{
    private SparseMatrix _sparseMatrix;
    [SetUp]
    public void Setup()
    {
        _sparseMatrix = new SparseMatrix
        {
            N = 5,
            IG = new[] { 0, 0, 0, 2, 5, 6 },
            JG = new[] { 0, 1, 0, 1, 2, 3 },
            DI = new[] { 2.0, 2.0, 2.0, 2.0, 2.0 },
            GG = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
        };
    }

    [Test]
    public void DecompositionTest()
    {
        var actualSparseMatrix = new SparseMatrix
        {
            N = 5,
            IG = new[] { 0, 0, 0, 2, 5, 6 },
            JG = new[] { 0, 1, 0, 1, 2, 3 },
            DI = new[] { Math.Sqrt(2.0), Math.Sqrt(2.0), 1.0, 1.0, 1.0 },
            GG = new[] { 0.70710678118654746, 0.70710678118654746, 0.70710678118654746, 0.70710678118654746, 2.2204460492503131E-16, 1.0 },
        };
        var expectedSparseMatrix = IncompleteCholeskyDecomposition.Decomposition(_sparseMatrix);
        CollectionAssert.AreEquivalent(expectedSparseMatrix.GG, actualSparseMatrix.GG);
        CollectionAssert.AreEquivalent(expectedSparseMatrix.DI, actualSparseMatrix.DI);
    }
}