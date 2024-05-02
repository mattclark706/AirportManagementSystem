public static class Clock
{
    public static DateTime currentTime;

    //clock constructer
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

    // method for increasing time
    public static void AdvanceTime(TimeSpan duration)
    {
        currentTime = currentTime.Add(duration);
    }
}