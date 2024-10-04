using Microsoft.AspNetCore.Mvc;
using WebIdentityApp.Models;
using WebIdentityApp.Services;

namespace WebIdentityApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = await _authService.RegisterAsync(model.Email, model.Password);
            return CreatedAtAction(nameof(Login), new { email = user.Email }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _authService.LoginAsync(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpGet("confirm-email/{userId}")]
        public async Task<IActionResult> ConfirmEmail(string userId)
        {
            var success = await _authService.ConfirmEmailAsync(userId);
            return success ? Ok("Email confirmed.") : BadRequest("Email confirmation failed.");
        }
    }
}
