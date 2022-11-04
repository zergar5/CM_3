using System.Globalization;
using CM_3.Tools;

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
        var text = streamReader.ReadToEnd();
        text = StringFormatter.Format(text).Replace('.', ',');
        var vector = text.Split(' ').Select(double.Parse).ToArray();
        return vector;
    }

    public int[] ReadInt(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        var text = streamReader.ReadToEnd();
        text = StringFormatter.Format(text);
        var stat = 0;
        var vector = text.Split(' ').Select(int.Parse).ToArray();
        if (vector[0] != 1) return vector;
        vector = vector.Select(x => x - 1).ToArray();
        return vector;
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