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
    public class PaymentMethodController : ControllerBase
    {
        private PaymentMethodService _pmService;
        public PaymentMethodController(PaymentMethodService pmService)
        {
            _pmService = pmService;
        }

        [HttpPost][Route("")]
        public async Task<ActionResult> CreatePaymentMethod([FromBody] CreatePaymentMethodDto paymentMethodDto)
        {
            try
            {
                ValidationResult validateCreatePM = new CreatePaymentMethodValidator().Validate(paymentMethodDto);
                if(!validateCreatePM.IsValid) return BadRequest(validateCreatePM.Errors);

                var res = await _pmService.CreatePaymentMethod(paymentMethodDto);
                return CreatedAtAction(nameof(CreatePaymentMethod),res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut][Route("{pmId}")]
        public async Task<ActionResult> UpdatePaymentMethod([FromRoute]string pmId, [FromBody]UpdatePaymentMethodDto paymentMethodDto)
        {
            try
            {
                ValidationResult validateUpdatePM = new UpdatePaymentMethodValidator().Validate(paymentMethodDto);
                if(!validateUpdatePM.IsValid) return BadRequest(validateUpdatePM.Errors);

                var res = await _pmService.UpdatePaymentMethod(pmId, paymentMethodDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete][Route("{pmId}")]
        public async Task<ActionResult> DeletePaymentMethod([FromRoute]string pmId)
        {
            try
            {
                var res = await _pmService.DeletePaymentMethod(pmId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet][Route("{storeId}")]
        public async Task<ActionResult> GetPaymentMethods([FromRoute] string storeId)
        {
            try
            {
                var res = await _pmService.GetPaymentMethods(storeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
