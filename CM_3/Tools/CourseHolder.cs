namespace CM_3.Tools;

public static class CourseHolder
{
    public static void GetInfo(int iteration, double residual)
    {
        Console.Write($"Iteration number: {iteration}, residual: {residual}\r");
    }
}