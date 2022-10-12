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
            _bookingViewModel.StartDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            _bookingViewModel.EndDate = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
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

                DateTime End = (DateTime)rezervare.EndDate;
                DateTime Start = (DateTime)rezervare.StartDate;

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
                else if (Start.Day != End.Day)
                {
                    ModelState.AddModelError(nameof(BookingViewModel.EndDate), "StartDate si EndDate trebuie sa fie in aceeasi zi");
                    return View(model);
                }
                
                if (End.Hour == Start.Hour)
                {
                    if(End.Minute - Start.Minute < 30)
                    {
                        ModelState.AddModelError(nameof(BookingViewModel.EndDate), "O rezervare trebuie sa fie de minim 30 minute");
                        return View(model);
                    }
                }
                else
                {
                    if(Start.Minute - End.Minute > 30)
                    {
                        ModelState.AddModelError(nameof(BookingViewModel.EndDate), "O rezervare trebuie sa fie de minim 30 minute");
                        return View(model);
                    }
                   
                }

                foreach (var item in _statiiIncarcareContext.Rezervares)
                {
                    if(item.IdPriza == rezervare.IdPriza)
                    {
                        if (item.StartDate == rezervare.StartDate && item.EndDate == rezervare.EndDate)
                        {
                            ModelState.AddModelError(nameof(BookingViewModel.EndDate), "Exista deja o rezervare fix la aceste ore");
                            return View(model);
                        }    
                        if (item.StartDate < rezervare.StartDate && item.EndDate > rezervare.StartDate && item.StartDate < rezervare.EndDate && item.EndDate > rezervare.EndDate)
                        {
                            ModelState.AddModelError(nameof(BookingViewModel.EndDate), "Intervalul selectat este cuprins intr-o alta rezervare");
                            return View(model);
                        }
                        if (item.StartDate >= rezervare.StartDate && item.StartDate <= rezervare.EndDate && item.EndDate >= rezervare.EndDate)
                        {
                            ModelState.AddModelError(nameof(BookingViewModel.EndDate), "Ora de sfarsit este cuprinsa intr-o alta rezervare");
                            return View(model);
                        }
                        if (item.StartDate <= rezervare.StartDate && item.EndDate >= rezervare.StartDate && item.EndDate <= rezervare.EndDate)
                        {
                            ModelState.AddModelError(nameof(BookingViewModel.StartDate), "Ora de inceput este cuprinsa intr-o alta rezervare");
                            return View(model);
                        }
                        if (item.StartDate >= rezervare.StartDate && item.EndDate >= rezervare.StartDate && item.EndDate <= rezervare.EndDate)
                        {
                            ModelState.AddModelError(nameof(BookingViewModel.EndDate), "Intre aceste ore exista o rezervare");
                            return View(model);
                        }
                    }
                    
                }

                _statiiIncarcareContext.Add(rezervare);
                _statiiIncarcareContext.SaveChanges();
                return RedirectToAction("Details", "Statii", new { id = model.IdStatie });
            }
            return View(model);
        }
    }
}
