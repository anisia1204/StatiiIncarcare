namespace StatiiIncarcare.Models.ViewModels
{
    public class WeekCalendar
    {
        public List<DayCalendar> Days { get; set; }
        public int IdPriza { get; set; }

        public DateTime CurrentWeek { get; set; }   
    }
}
