public static class Clock
{
    public static DateTime currentTime;

    static Clock()
    {
        currentTime = DateTime.Now;
        while (currentTime.DayOfWeek != DayOfWeek.Monday) 
        {
            currentTime = currentTime.AddDays(1);
        }
        currentTime = currentTime.Date;
    }

    public static DateTime CurrentTime => currentTime;

    public static void AdvanceTime(TimeSpan duration)
    {
        currentTime = currentTime.Add(duration);
    }
}