using CM_3.IO;

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

    public void GenerateHilbert(MatrixIO matrixIO, string fileName)
    {
        double[,] hilbertMatrix = new double[20, 20];
        matrixIO.ReadSize(this, fileName);
    }
}