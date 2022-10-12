using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StatiiIncarcare.Models;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
using System.Diagnostics;

namespace StatiiIncarcare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        public HomeController(ILogger<HomeController> logger, StatiiIncarcareContext statiiIncarcareContext)
        {
            _logger = logger;
            _statiiIncarcareContext = statiiIncarcareContext;
        }

        public IActionResult Index()
        {
            List<Chart> charts = new List<Chart>();

            foreach (var item in _statiiIncarcareContext.Staties.Include(s => s.Prizas).ThenInclude(x => x.Rezervares))
            {
                var chart = new Chart();
                chart.Nume = item.Nume;
                var rezervari = 0;

                var PrizeleStatiei = item.Prizas.Where(x => x.IdStatie == item.IdStatie).ToList();

                foreach (var prizas in PrizeleStatiei)
                {
                    var nrRezervariPerPriza = prizas.Rezervares.Count();
                    rezervari = rezervari + nrRezervariPerPriza;
                }
                chart.NumarRezervari = rezervari;
                charts.Add(chart);
            }

            ViewBag.Chart = JsonConvert.SerializeObject(charts);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}