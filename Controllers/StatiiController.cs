using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using Microsoft.EntityFrameworkCore;
using PagedList;

namespace StatiiIncarcare.Controllers
{
    public class StatiiController : Controller
    {
        private readonly StatiiIncarcareContext _statiiIncarcareContext;
        public StatiiController(StatiiIncarcareContext statiiIncarcareContext)
        {
            _statiiIncarcareContext = statiiIncarcareContext;
        }
        public IActionResult GetStatii(string sortOrder, string searchString)
        {
            ViewBag.NumeSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AdresaSortParm = sortOrder == "Adresa" ? "adress_desc" : "Adresa";
            ViewBag.SearchString = searchString;

            var statii = from s in _statiiIncarcareContext.Staties
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                statii = statii.Where(s => s.Nume.Contains(searchString)
                                       || s.Adresa.Contains(searchString)
                                       || s.Oras.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    statii = statii.OrderByDescending(s => s.Nume);
                    break;
                case "Adresa":
                    statii = statii.OrderBy(s => s.Adresa);
                    break;
                case "adress_desc":
                    statii = statii.OrderByDescending(s => s.Adresa);
                    break;
                default:
                    statii = statii.OrderBy(s => s.Nume);
                    break;
            }

            return View(statii.ToList());
        }

        //GET
        [HttpGet]
        public IActionResult GetStatii()
        {
            ViewBag.NumeSortParm = "name_desc";
            ViewBag.AdresaSortParm = "adress_desc";
            return View(_statiiIncarcareContext.Staties);
        }

        // GET: StatiiIncarcare/CreateStatie
        public IActionResult CreateStatie()
        {
            return View(new Statie());
        }

        // POST: StatiiIncarcare/CreateStatie
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
            var statie = _statiiIncarcareContext.Staties.
                Include(s => s.Prizas).
                ThenInclude(x => x.IdTipNavigation).
                FirstOrDefault(s => s.IdStatie == id);

            if (statie == null)
            {
                return NotFound();
            }
            return View(statie);
        }

        // POST: StatiiIncarcare/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var statie = await _statiiIncarcareContext.Staties.FindAsync(id);
            _statiiIncarcareContext.Staties.Remove(statie);
            await _statiiIncarcareContext.SaveChangesAsync();
            return RedirectToAction(nameof(GetStatii));
        }
    }
}
