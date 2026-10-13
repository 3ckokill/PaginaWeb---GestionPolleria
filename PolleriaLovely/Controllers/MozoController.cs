using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;

namespace PolleriaLovely.Controllers
{
    public class MozoController : Controller
    {
        [Authorize(Roles = "Mozo")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
