using System.Globalization;

namespace CM_3.IO;

public class VectorIO
{
    private readonly string _path;
    public VectorIO(string path)
    {
        _path = path;
    }
    public double[] ReadDouble(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        return streamReader.ReadLine().Replace('.', ',').Split(' ').Select(Convert.ToDouble).ToArray();
    }

    public int[] ReadInt(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        return streamReader.ReadLine().Replace('.', ',').Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }

    public void Write(double[] vector, string fileName)
    {
        using var streamWriter = new StreamWriter(_path + fileName);
        foreach (var element in vector)
        {
            streamWriter.WriteLine(element);
        }
    }
}