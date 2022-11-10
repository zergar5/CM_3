using CM_3.Tools.Precondition;

namespace CM_3_Tests;

public class PreconditionerTest
{
    private double[] _di;

    [SetUp]
    public void Setup()
    {
        _di = new[] { 2.0, 2.0, 2.0, 2.0, 2.0 };
    }

    [Test]
    public void MultiplyMatrixOnVectorTest()
    {
        var actual = new[]
        {
            Math.Sqrt(1.0 / 2.0), Math.Sqrt(1.0 / 2.0), Math.Sqrt(1.0 / 2.0), Math.Sqrt(1.0 / 2.0), Math.Sqrt(1.0 / 2.0)
        };
        var expected = Preconditioner.CreateMatrix(_di);
        CollectionAssert.AreEqual(expected, actual);
    }
}