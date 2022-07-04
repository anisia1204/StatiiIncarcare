using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;

namespace StatiiIncarcare.Controllers
{
    public class PrizeController : Controller
    {
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        private PrizeViewModel _prizeViewModel;
        public PrizeController(StatiiIncarcareContext statiiIncarcareContext)
        {
            _statiiIncarcareContext = statiiIncarcareContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePriza(int id)
        {
            _prizeViewModel = new PrizeViewModel();
            _prizeViewModel.IdStatie = id;
            _prizeViewModel.TipPrize = _statiiIncarcareContext.TipPrizas.ToList();
            return View(_prizeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePriza(PrizeViewModel model)
        {
            var priza = new Priza();
            priza.IdStatie = model.IdStatie;
            priza.IdTip = model.IdTipPrize;
            _statiiIncarcareContext.Add(priza);
            _statiiIncarcareContext.SaveChanges();
            return RedirectToAction("Details", "Statii", new { id = model.IdStatie });
        }
    }
}
