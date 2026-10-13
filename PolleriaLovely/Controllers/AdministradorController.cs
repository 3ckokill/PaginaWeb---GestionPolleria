using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;

namespace PolleriaLovely.Controllers
{
    public class AdministradorController : Controller
    {
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
