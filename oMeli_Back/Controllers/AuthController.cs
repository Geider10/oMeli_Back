using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services;
using oMeli_Back.Validators;
using FluentValidation;
using FluentValidation.Results;
using oMeli_Back.DTOs.Auth;
namespace oMeli_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> SignUp([FromBody]SignUpDto signUpDto)
        {
            try
            {
                ValidationResult validator = new SignUpValidator().Validate(signUpDto);
                if (!validator.IsValid) return BadRequest(validator.Errors);
                var response = await _authService.SignUp(signUpDto);

                return CreatedAtAction(nameof(SignUp),response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LogIn([FromBody]LogInDto logInDto)
        {
            try
            {
                ValidationResult validator = new LogInValidator().Validate(logInDto);
                if (!validator.IsValid) return BadRequest(validator.Errors);
                var response = await _authService.LogIn(logInDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
