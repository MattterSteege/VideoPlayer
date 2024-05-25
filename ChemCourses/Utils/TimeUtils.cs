namespace ChemCourses.Utils;

public static class TimeUtils
{
    //seconds to hh:mm:ss or mm:ss or ss
    public static string SecondsToTime(int seconds)
    {
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int secs = seconds % 60;

        if (hours > 0)
        {
            return $"{hours}:{minutes:00}:{secs:00}";
        }
        if (minutes > 0)
        {
            return $"{minutes}:{secs:00}";
        }
        return $"{secs}";
    }
}