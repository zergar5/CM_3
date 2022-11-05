using CM_3.Models;
using System.Globalization;

namespace CM_3.IO;

public class ParametersIO
{
    private readonly string _path;
    public ParametersIO(string path)
    {
        _path = path;
    }
    public (int, double) ReadMethodParameters(string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        var paramsIn = streamReader.ReadLine().Replace('.', ',').Split(' ');
        var parameters = (Convert.ToInt32(paramsIn[1]), Convert.ToDouble(paramsIn[2]));
        return parameters;
    }
}