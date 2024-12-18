using System.Diagnostics;
using BandungHub.Data; // Namespace untuk BandungHubContext
using BandungHub.Models; // Namespace untuk model Departemen
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BandungHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly BandungHubContext _context;

        public HomeController(BandungHubContext context)
        {
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var departemens = await _context.Departement.ToListAsync();
            return View(departemens);
        }
    }
}
