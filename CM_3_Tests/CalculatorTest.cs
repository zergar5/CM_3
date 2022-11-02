using CM_3.Models;
using CM_3.Tools;

namespace CM_3_Tests
{
    public class CalculatorTest
    {
        private SparseMatrix _sparseMatrix;
        private double[] _pr;

        [SetUp]
        public void Setup()
        {
            _sparseMatrix = new SparseMatrix
            {
                N = 5,
                IG = new[] {0, 0, 0, 2, 4, 6},
                JG = new[] {0, 1, 1, 2, 0, 2},
                DI = new[] { 2.0, 2.0, 2.0, 2.0, 2.0},
                GG = new[] {1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            };
            _pr = new[] {1.0, 1.0, 1.0, 1.0, 1.0};
        }

        [TestCase(new[] { 4.0, 4.0, 6.0, 4.0, 4.0 })]
        public void MultiplyMatrixOnVectorTest(double[] actual)
        {
            var expected = Calculator.MultiplyMatrixOnVector(_sparseMatrix, _pr);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new[] { 5.0, 5.0, 5.0, 5.0, 5.0 })]
        public void MultiplyVectorOnNumberTest(double[] actual)
        {
            var expected = Calculator.MultiplyVectorOnNumber(_pr, 5.0);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(
            new[] {6.0, 6.0, 6.0, 6.0, 6.0},
            new[] { 5.0, 5.0, 5.0, 5.0, 5.0 })]
        public void SubtractVectorsTest(double[] vectorA, double[] actual)
        {
            var expected = Calculator.SubtractVectors(vectorA, _pr);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(
            new[] { 6.0, 6.0, 6.0, 6.0, 6.0 },
            new[] { 7.0, 7.0, 7.0, 7.0, 7.0 })]
        public void SumVectorsTest(double[] vectorA, double[] actual)
        {
            var expected = Calculator.SumVectors(vectorA, _pr);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(
            new[] { 6.0, 6.0, 6.0, 6.0, 6.0 },
            30.0)]
        public void ScalarProductTest(double[] vectorA, double actual)
        {
            var expected = Calculator.ScalarProduct(vectorA, _pr);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(
            new[] { 5.0, 5.0, 5.0, 5.0, 5.0 })]
        public void CalcNormTest(double[] vectorA)
        {
            var actual = Math.Sqrt(125.0);
            var expected = Calculator.CalcNorm(vectorA);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(
            new[] { 6.0, 6.0, 6.0, 6.0, 6.0 },
            new[] { 12.0, 12.0, 12.0, 12.0, 12.0 })]
        public void MultiplyDiagonalOnVectorTest(double[] vectorA, double[] actual)
        {
            var expected = Calculator.MultiplyDiagonalOnVector(_sparseMatrix.DI, vectorA);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}