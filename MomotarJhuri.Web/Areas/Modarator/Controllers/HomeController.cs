using Microsoft.AspNetCore.Mvc;

namespace MomotarJhuri.Web.Areas.Modarator.Controllers
{
    [Area("Modarator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
