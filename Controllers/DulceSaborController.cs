using Microsoft.AspNetCore.Mvc;
using DulceSaborOnline___WEB.Models;

namespace DulceSaborOnline___WEB.Controllers
{
    public class DulceSaborController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }
    }
}
