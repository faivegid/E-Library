using GBLAC.Models.DTOs;
using GBLAC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GBLAC.MVC.Controllers.MVCRepository
{

    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            this._logger = logger;
            APIConnection.Initialise();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {
            string baseUri = string.Format("{0}://{1}/api/account/login", Request.Scheme, Request.Host);
            var result = await UserProcessor.PostAsync(baseUri, login);
            var user = JsonConvert.DeserializeObject<UserDTO>(result);
            return View();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisteModel register)
        {
            string baseUri = string.Format("{0}://{1}/api/account/login", Request.Scheme, Request.Host);
            var result = await UserProcessor.PostAsync(baseUri, register);
            var user = JsonConvert.DeserializeObject<UserDTO>(result);
            return View();
        }
    }
}
