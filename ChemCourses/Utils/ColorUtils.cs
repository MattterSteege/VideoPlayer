namespace ChemCourses.Utils;

public static class ColorUtils
{
    private static readonly Random Random = new();
    
    public static string GenerateRandomColorHex()
    {
        const string letters = "0123456789ABCDEF";
        string color = "#";
        for (int i = 0; i < 6; i++)
        {
            color += letters[Random.Next(letters.Length)];
        }
        return color;
    }
}