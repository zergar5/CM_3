using CM_3.Models;

namespace CM_3.IO;

public class MatrixIO
{
    private readonly string _path;
    private readonly VectorIO _vectorIO;

    public MatrixIO(string path)
    {
        _path = path;
        _vectorIO = new VectorIO(path);
    }
    public void Read(SparseMatrix sparseMatrix, SourcesForMatrix sourcesForMatrix)
    {
        ReadSize(sparseMatrix, sourcesForMatrix.NFile);
        ReadRowIndexes(sparseMatrix, sourcesForMatrix.IGFile);
        ReadColumnIndexes(sparseMatrix, sourcesForMatrix.JGFile);
        ReadElements(sparseMatrix, sourcesForMatrix.GGFile);
        ReadDiagonal(sparseMatrix, sourcesForMatrix.DIFile);
    }

    public void ReadSize(SparseMatrix sparseMatrix, string nFile)
    {
        using var streamReader = new StreamReader(_path + nFile);
        sparseMatrix.N = int.Parse(streamReader.ReadLine().Split(' ')[0]);
    }

    private void ReadRowIndexes(SparseMatrix sparseMatrix, string igFile)
    {
        sparseMatrix.IG = _vectorIO.ReadInt(igFile);
    }

    private void ReadColumnIndexes(SparseMatrix sparseMatrix, string jgFile)
    {
        sparseMatrix.JG = _vectorIO.ReadInt(jgFile);
    }

    private void ReadElements(SparseMatrix sparseMatrix, string ggFile)
    {
        sparseMatrix.GG = _vectorIO.ReadDouble(ggFile);
    }

    private void ReadDiagonal(SparseMatrix sparseMatrix, string diFile)
    {
        sparseMatrix.DI = _vectorIO.ReadDouble(diFile);
    }
}