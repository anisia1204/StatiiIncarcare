using StatiiIncarcare.Models.DB;

namespace StatiiIncarcare.Models.ViewModels
{
    public class DayCalendar
    {
        public DateTime date { get; set; }
        public List<Rezervare> rezervari { get; set; }
}
}
