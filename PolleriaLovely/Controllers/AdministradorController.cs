using Microsoft.AspNetCore.Mvc;

namespace PolleriaLovely.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
