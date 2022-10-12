using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;

namespace StatiiIncarcare.Controllers
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
    public class DisponibilitateController : Controller
    {
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        private static DateTime _currentWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);

        public DisponibilitateController(StatiiIncarcareContext statiiIncarcareContext)
        {
            _statiiIncarcareContext = statiiIncarcareContext;
        }
        public IActionResult Index(int id)
        {

            WeekCalendar weekCalendar = new WeekCalendar();
            weekCalendar.Days = new List<DayCalendar>();
            var date = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            _currentWeek = date;

            for(int i = 1; i <= 7; i++)
            {
                var rezervariPerDate = _statiiIncarcareContext.Rezervares.Where(x => x.IdPriza == id && x.StartDate.Value.Date == date.Date).ToList();
                DayCalendar rez = new DayCalendar();
                rez.rezervari = rezervariPerDate;
                rez.date = date;
                weekCalendar.Days.Add(rez);
                date =  date.AddDays(1);
            }

            weekCalendar.IdPriza = id;
            weekCalendar.CurrentWeek = _currentWeek;
            

            return View(weekCalendar);
        }

        public IActionResult NextWeek(int id)
        {
            _currentWeek = _currentWeek.AddDays(7);

            WeekCalendar weekCalendar = new WeekCalendar();
            weekCalendar.Days = new List<DayCalendar>();
            var date = _currentWeek;


            for (int i = 1; i <= 7; i++)
            {
                var rezervariPerDate = _statiiIncarcareContext.Rezervares.Where(x => x.IdPriza == id && x.StartDate.Value.Date == date.Date).ToList();
                DayCalendar rez = new DayCalendar();
                rez.rezervari = rezervariPerDate;
                rez.date = date;
                weekCalendar.Days.Add(rez);
                date = date.AddDays(1);
            }

            weekCalendar.IdPriza = id;

            weekCalendar.CurrentWeek = _currentWeek;

            return View("Index",weekCalendar);
        }

        public IActionResult PrevWeek(int id)
        {
            _currentWeek = _currentWeek.AddDays(-7);

            WeekCalendar weekCalendar = new WeekCalendar();
            weekCalendar.Days = new List<DayCalendar>();
            var date = _currentWeek;


            for (int i = 1; i <= 7; i++)
            {
                var rezervariPerDate = _statiiIncarcareContext.Rezervares.Where(x => x.IdPriza == id && x.StartDate.Value.Date == date.Date).ToList();
                DayCalendar rez = new DayCalendar();
                rez.rezervari = rezervariPerDate;
                rez.date = date;
                weekCalendar.Days.Add(rez);
                date = date.AddDays(1);
            }

            weekCalendar.IdPriza = id;
            weekCalendar.CurrentWeek = _currentWeek;
            return View("Index", weekCalendar);
        }

    }
}
