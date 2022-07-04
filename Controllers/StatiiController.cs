using Microsoft.AspNetCore.Mvc;
using StatiiIncarcare.Models.DB;
using Microsoft.EntityFrameworkCore;

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
