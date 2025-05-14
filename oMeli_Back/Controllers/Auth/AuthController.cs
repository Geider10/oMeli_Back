using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Validators.Auth;
using FluentValidation;
using FluentValidation.Results;
using oMeli_Back.DTOs.Auth;
using oMeli_Back.Services.Auth;
namespace oMeli_Back.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        [HttpPost][Route("signup")]
        public async Task<ActionResult> SignUp([FromBody]SignUpDto signUpDto)
        {
            try
            {
                ValidationResult validateSignUpDto = new SignUpValidator().Validate(signUpDto);
                if (!validateSignUpDto.IsValid) return BadRequest(validateSignUpDto.Errors);
                
                var res = await _authService.SignUp(signUpDto);
                return CreatedAtAction(nameof(SignUp),res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost][Route("login")]
        public async Task<ActionResult> LogIn([FromBody]LogInDto logInDto)
        {
            try
            {
                ValidationResult validateLogInDto = new LogInValidator().Validate(logInDto);
                if (!validateLogInDto.IsValid) return BadRequest(validateLogInDto.Errors);
                
                var res = await _authService.LogIn(logInDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
