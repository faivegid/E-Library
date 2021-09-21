using GBLAC.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GBLAC.MVC.Controllers.MVCRepository
{

    public class LoginController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel login)
        {
            return View();
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Registration(UserRegisteModel register)
        {
            return View();
        }
    }
}
