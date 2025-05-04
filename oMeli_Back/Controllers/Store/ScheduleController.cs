using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services.Store;
using oMeli_Back.DTOs.Store;
using oMeli_Back.Validators.Store;
using FluentValidation;
using FluentValidation.Results;

namespace oMeli_Back.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private ScheduleService _scheduleService;
        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost][Route("")]
        public async Task<ActionResult> CreateSchedule([FromBody] CreateScheduleDto scheduleDto)
        {
            try
            {
                var validateCreateSchedule = new CreateScheduleValidator().Validate(scheduleDto);
                if (!validateCreateSchedule.IsValid) return BadRequest(validateCreateSchedule.Errors);

                var res = await _scheduleService.CreateSchedule(scheduleDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut][Route("{scheduleId}")]
        public async Task<ActionResult> UpdateSchedule([FromRoute]string scheduleId, [FromBody] UpdateScheduleDto scheduleDto)
        {
            try
            {
                ValidationResult validateUpdateScheduleDto = new UpdateScheduleValidator().Validate(scheduleDto);
                if (!validateUpdateScheduleDto.IsValid) return BadRequest(validateUpdateScheduleDto.Errors);
                var res = await _scheduleService.UpdateSchedule(scheduleId, scheduleDto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
