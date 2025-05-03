using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services.Store;
using oMeli_Back.DTOs.Store;
using oMeli_Back.Validators.Store;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace oMeli_Back.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private StoreService _storeService;
        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost][Route("")]
        public async Task<ActionResult> CreateStore([FromBody] CreateStoreDto storeDto)
        {
            try
            {
                ValidationResult validateCreateStore = new CreateStoreValidator().Validate(storeDto);
                if (!validateCreateStore.IsValid) return BadRequest(validateCreateStore.Errors);

                var res = await _storeService.CreateStore(storeDto);
                return CreatedAtAction(nameof(CreateStore), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut][Route("{storeId}")]
        public async Task<ActionResult> UpdateStore([FromBody] UpdateStoreDto storeDto, [FromRoute] string storeId)
        {
            try
            {
                ValidationResult validateUpdateStore = new UpdateStoreValidator().Validate(storeDto);
                if (!validateUpdateStore.IsValid) return BadRequest(validateUpdateStore.Errors);

                var res = await _storeService.UpdateStore(storeDto, storeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("user/{userId}")]
        public async Task<ActionResult> GetStoreByUserId([FromRoute] string userId)
        {
            try
            {
                var res = await _storeService.GetStoreByUserIdDto(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
