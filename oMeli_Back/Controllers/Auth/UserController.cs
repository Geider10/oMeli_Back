using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services.Auth;
using oMeli_Back.DTOs.Auth;
using oMeli_Back.Validators.Auth;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace oMeli_Back.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet][Route("{userId}")]
        public async Task<ActionResult> GetUserById([FromRoute]string userId)
        {
            try
            {
                var res = await _userService.GetUserById(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut][Route("{userId}")]
        public async Task<ActionResult> UpdateUser([FromRoute]string userId, [FromBody]UpdateUserDto userDto)
        {
            try
            {
                ValidationResult validateUserDto = new UpdateUserValidator().Validate(userDto);
                if (!validateUserDto.IsValid) return BadRequest(validateUserDto.Errors);

                var res = await _userService.UpdateUser(userId, userDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut][Route("{userId}/password")]
        public async Task<ActionResult> UpdatePassword([FromRoute]string userId, [FromBody]UpdatePasswordDto passwordDto)
        {
            try
            {
                ValidationResult validatePasswordDto = new UpdatePasswordValidator().Validate(passwordDto);
                if (!validatePasswordDto.IsValid) return BadRequest(validatePasswordDto.Errors);

                var res = await _userService.UpdatePassword(userId, passwordDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
