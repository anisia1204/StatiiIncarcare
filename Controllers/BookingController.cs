using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using StatiiIncarcare.Models.ViewModels;
namespace StatiiIncarcare.Controllers
{
    public class BookingController : Controller
    {
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        private BookingViewModel _bookingViewModel;
        public BookingController(StatiiIncarcareContext statiiIncarcareContext)
        {
            _statiiIncarcareContext = statiiIncarcareContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookNow(int id)
        {
            _bookingViewModel = new BookingViewModel();
            _bookingViewModel.IdPriza = id;
            _bookingViewModel.IdStatie = _statiiIncarcareContext.Prizas.Where(p => p.IdPriza == id).FirstOrDefault().IdStatie;
            _bookingViewModel.StartDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            _bookingViewModel.EndDate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            return View(_bookingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookNow(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var rezervare = new Rezervare();
                rezervare.IdPriza = model.IdPriza;
                rezervare.StartDate = model.StartDate;
                rezervare.EndDate = model.EndDate;
                rezervare.NrMasina = model.NrMasina;
                if (rezervare.StartDate >= rezervare.EndDate)
                {
                    ModelState.AddModelError(nameof(BookingViewModel.EndDate), "Ora de sfarsit trebuie sa fie dupa ora de inceput");
                    return View(model);
                }
                else if (rezervare.StartDate < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(BookingViewModel.StartDate), "Ora de inceput e mai mica decat ora actuala");
                    return View(model);
                }
                else
                {
                    _statiiIncarcareContext.Add(rezervare);
                    _statiiIncarcareContext.SaveChanges();
                    return RedirectToAction("Details", "Statii", new { id = model.IdStatie });
                }
            }
            return View(model);
        }
    }
}
