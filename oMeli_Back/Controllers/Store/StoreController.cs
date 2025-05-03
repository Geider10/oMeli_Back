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
    }
}
