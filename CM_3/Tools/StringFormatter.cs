namespace CM_3.Tools;

public static class StringFormatter
{
    public static string Format(string originalString)
    {
        var formattedString = System.Text.RegularExpressions.Regex.Replace(originalString, @"\s+", " ");
        formattedString = formattedString.Trim().Replace("\n", "").Replace("\r", "");
        return formattedString;
    }
}