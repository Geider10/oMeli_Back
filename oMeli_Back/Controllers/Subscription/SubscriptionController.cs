using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.DTOs.Subscription;
using oMeli_Back.Validators.Subscription;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using oMeli_Back.Services.Subscription;

namespace oMeli_Back.Controllers.Subscription
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private SubscriptionService _subscriptionService;
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet][Route("user/{userId}")]
        public async Task<ActionResult> GetStoreByUser([FromRoute] string userId)
        {
            try
            {
                var subscription = await _subscriptionService.GetStoreByUser(userId);
                return Ok(subscription);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost][Route("")]
        public async Task<ActionResult> CreateSubscription ([FromBody] CreateSubscriptionDto createDto)
        {
            try
            {
                ValidationResult validateCreateSubscription = new CreateSubscriptionValidator().Validate(createDto);
                if (!validateCreateSubscription.IsValid) return BadRequest(validateCreateSubscription.Errors);

                var res = await _subscriptionService.CreateSubscription(createDto);
                return CreatedAtAction(nameof(CreateSubscription), res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut][Route("{subscriptionId}")]
        public async Task<ActionResult> UpdateSubscription([FromRoute] string subscriptionId,[FromBody] UpdateSubscriptionDto updateDto)
        {
            try
            {
                ValidationResult validateUpdateSubscription = new UpdateSubscriptionValidator().Validate(updateDto);
                if (!validateUpdateSubscription.IsValid) return BadRequest(validateUpdateSubscription.Errors);
                
                var res = await _subscriptionService.UpdateSubscription(updateDto, subscriptionId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
