namespace WpfApp1;

[System.Serializable]
public class TimeCounter
{
    public int Seconds { get; set; }
    public int Minutes { get; set; }
    public int Hours { get; set; }

    public string Time
    {
        get
        {
            return $"{Hours.ToString("00")}:{Minutes.ToString("00")}:{Seconds.ToString("00")}";
        }
    }

    public void TickSecond()
    {
        Seconds++;
        if (Seconds >= 60)
        {
            Seconds -= 60;
            Minutes++;
        }

        if (Minutes >= 60)
        {
            Minutes -= 60;
            Hours++;
        }
    }
}