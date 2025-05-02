using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oMeli_Back.Services;

namespace oMeli_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private PlanService _planService;
        public PlanController(PlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetPlans()
        {
            try
            {
                var plans = await _planService.GetPlans();
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{planId}")]
        public async Task<ActionResult> GetPlanById([FromRoute] string planId)
        {
            try
            {
                var plan = await _planService.GetPlanById(planId);
                return Ok(plan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
