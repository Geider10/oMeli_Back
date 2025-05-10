using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Validators.Store;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using oMeli_Back.DTOs.Interaction;
using oMeli_Back.Services.Interaction;
using oMeli_Back.Validators.Interaction;

namespace oMeli_Back.Controllers.Interaction
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpDelete][Route("store/{storeId}/user/{userId}")]
        public async Task<ActionResult> DeleteFollower([FromRoute] string storeId, [FromRoute] string userId)
        {
            try
            {
                var res = await _followerService.DeleteFollower(storeId, userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("store/{storeId}")]
        public async Task<ActionResult> GetFollowersByStore([FromRoute] string storeId)
        {
            try
            {
                var res = await _followerService.GetFollowersByStore(storeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("user/{userId}")]
        public async Task<ActionResult> GetStoresFollwedByUser([FromRoute] string userId)
        {
            try
            {
                var res = await _followerService.GetStoresFollowedByUser(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
