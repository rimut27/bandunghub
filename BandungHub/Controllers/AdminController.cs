using System.Diagnostics;
using BandungHub.Data; // Namespace untuk BandungHubContext
using BandungHub.Models; // Namespace untuk model Departemen
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BandungHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly BandungHubContext _context;

        // Constructor
        public AdminController(ILogger<AdminController> logger, BandungHubContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var departemens = await _context.Departement.ToListAsync();
            return View(departemens);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nama,Link")] Departemen departemen, IFormFile? Icon)
        {
            if (ModelState.IsValid)
            {
                // Check if an icon file is uploaded
                if (Icon != null && Icon.Length > 0)
                {
                    // Save the file to the wwwroot/uploads folder
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", Icon.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Icon.CopyToAsync(stream);
                    }

                    // Save the relative path or filename in the departemen's Icon property
                    departemen.Icon = "/uploads/" + Icon.FileName;
                }

                // Add the new departemen to the database
                _context.Add(departemen);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(departemen);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departemen = await _context.Departement.FindAsync(id);
            if (departemen == null)
            {
                return NotFound();
            }
            return View(departemen);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,Link,Icon")] Departemen departemen)
        {
            if (id != departemen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departemen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartemenExists(departemen.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departemen);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departemen = await _context.Departement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departemen == null)
            {
                return NotFound();
            }

            return View(departemen);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departemen = await _context.Departement.FindAsync(id);
            if (departemen != null)
            {
                _context.Departement.Remove(departemen);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartemenExists(int id)
        {
            return _context.Departement.Any(e => e.Id == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
