using DulceSaborOnline___WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DulceSaborOnline___WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly DScontext _context;

        public HomeController(DScontext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var itemsMenu = _context.items_menu.ToList();

            var combos = _context.combos.ToList();
            ViewBag.Combos1 = combos.ToList();
            ViewBag.Combos = combos.Skip(1).ToList();

            return View(itemsMenu);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
