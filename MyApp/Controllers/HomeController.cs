using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Creatives.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Creative creative)
        {
            db.Creatives.Add(creative);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var creative = await db.Creatives.FirstOrDefaultAsync(c => c.Id == id);
            if (creative == null)
                return NotFound();

            return View(creative);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var creative = await db.Creatives.FirstOrDefaultAsync(c => c.Id == id);
            if (creative == null)
                return NotFound();

            return View(creative);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await db.Creatives.FindAsync(id);
            db.Creatives.Remove(movie);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
