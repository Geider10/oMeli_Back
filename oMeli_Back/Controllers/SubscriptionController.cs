using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services;
using oMeli_Back.DTOs.Subscription;
using oMeli_Back.Validators.Subscription;
using FluentValidation.Results;
using FluentValidation;

namespace oMeli_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private SubscriptionService _subscriptionService;
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create([FromBody] CreateDto createDto)
        {
            try
            {
                ValidationResult validation = new CreateValidator().Validate(createDto);
                if (!validation.IsValid) return BadRequest(validation.Errors);
                var response = await _subscriptionService.Create(createDto);

                return CreatedAtAction(nameof(Create), response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] UpdateDto updateDto)
        {
            try
            {
                ValidationResult validation = new UpdateValidator().Validate(updateDto);
                if (!validation.IsValid) return BadRequest(validation.Errors);
                var response = await _subscriptionService.Update(updateDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
