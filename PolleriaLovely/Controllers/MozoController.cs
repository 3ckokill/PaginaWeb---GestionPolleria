using Microsoft.AspNetCore.Mvc;

namespace PolleriaLovely.Controllers
{
    public class MozoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
