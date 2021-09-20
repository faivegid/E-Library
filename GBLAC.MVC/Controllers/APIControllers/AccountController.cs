using AutoMapper;
using GBLAC.Models.DTOs;
using GBLAC.Repository.APIUnitOfWork.Interfaces;
using GBLAC.Services.APIServices.Interfaces;
using GBLAC.Services.TokenGeneration.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GBLAC.MVC.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        public AccountController(
            ILogger<AccountController> logger,
            IUnitOfWork unitOfWork,
            IUserService userService,
             ITokenGenerator tokenGenerator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync(LoginDTO login)
        {
            var userDTO = await _userService.LoginUserAsync(login);
            HttpContext.Session.SetString("Token", userDTO.Token);
            return Ok(userDTO);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync(RegisterationDTO request)
        {
            var userDTO = await _userService.RegisterUserAsync(request);
            return Created("api/User", userDTO);
        }
    }
}
