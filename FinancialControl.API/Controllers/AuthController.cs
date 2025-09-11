using FinancialControl.Application.DTOs;
using FinancialControl.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var newUser = await _userService.Register(registerDto);

            if (newUser == null)
            {
                return Conflict("O e-mail já está em uso.");
            }

            return Ok(new { message = "Usuário registrado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authService.AuthenticateAsync(loginDto);

            if (token == null)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            return Ok(new { token });
        }
    }
}