using CM_3.IO;
using CM_3.Tools;

namespace CM_3.Models;

public class SparseMatrix
{
    public int N { get; set; }
    public int[] IG { get; set; }
    public int[] JG { get; set; }
    public double[] GG { get; set; }
    public double[] DI { get; set; }

    public void CreateFromFiles(MatrixIO matrixIO, SourcesForMatrix sourcesForMatrix)
    {
        matrixIO.Read(this, sourcesForMatrix);
    }

    public void GenerateHilbert(MatrixIO matrixIO, HilbertGenerator hilbertGenerator, string fileName)
    {
        matrixIO.ReadSize(this, fileName);
        var sparseMatrix = hilbertGenerator.Generate(N);
        IG = sparseMatrix.IG;
        JG = sparseMatrix.JG;
        GG = sparseMatrix.GG;
        DI = sparseMatrix.DI;
    }
}