using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBLAC.MVC.Controllers.MVCRepository
{

    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
