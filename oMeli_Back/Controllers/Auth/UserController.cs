using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services.Auth;
using oMeli_Back.DTOs.Auth;
namespace oMeli_Back.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
