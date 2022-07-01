using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;

namespace StatiiIncarcare.Controllers
{
    public class StatiiController : Controller
    {
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        public StatiiController(StatiiIncarcareContext statiiIncarcareContext)
        {
            _statiiIncarcareContext = statiiIncarcareContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //GET
        [HttpGet]
        public IActionResult GetStatii()
        {
            return View(_statiiIncarcareContext.Staties);
        }

        // GET: StatiiIncarcare/CreateStatie
        public IActionResult CreateStatie()
        {
            return View(new Statie());
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStatie([Bind("IdStatie,Nume,Adresa,Oras")] Statie statie)
        {
            if (ModelState.IsValid)
            {
                _statiiIncarcareContext.Add(statie);
                _statiiIncarcareContext.SaveChanges();
                return RedirectToAction(nameof(GetStatii));
            }
            return View(statie);
        }

        public IActionResult Details(int id)
        {
            var statie = _statiiIncarcareContext.Staties.FirstOrDefault(s => s.IdStatie == id);
            return View(statie);
        }

    }
}
