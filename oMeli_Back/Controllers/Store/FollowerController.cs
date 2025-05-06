using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services.Store;
using oMeli_Back.DTOs.Store;
using oMeli_Back.Validators.Store;
using FluentValidation.Results;

namespace oMeli_Back.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private FollowerService _followerService;
        public FollowerController(FollowerService followerService)
        {
            _followerService = followerService;
        }

        [HttpPost][Route("")]
        public async Task<ActionResult> CreateFollower([FromBody] CreateFollowerDto followerDto)
        {
            try
            {
                ValidationResult ValidationFollowerDto = new CreateFollowerValidator().Validate(followerDto);
                if (!ValidationFollowerDto.IsValid) return BadRequest(ValidationFollowerDto.Errors);

                var res = await _followerService.CreateFollower(followerDto);
                return CreatedAtAction(nameof(CreateFollower), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
