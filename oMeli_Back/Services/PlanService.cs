using oMeli_Back.DTOs.Subscription;
using oMeli_Back.Context;
using Microsoft.EntityFrameworkCore;
namespace oMeli_Back.Services
{
    public class PlanService
    {
        private AppDBContext _context;
        public PlanService(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<GetPlanDto>> GetPlans()
        {
            var plans = await _context.Plans.Select(p => new GetPlanDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
            if( plans == null) throw new Exception("Plans not found");

            return plans;
        }
        public async Task<GetPlanDto> GetPlanById(string planId)
        {
            var plan = await _context.Plans.FirstOrDefaultAsync(p => p.Id == Guid.Parse(planId));
            if (plan == null) throw new Exception("Plan not found");
            var planDto = new GetPlanDto
            {
                Id = plan.Id,
                Name = plan.Name
            };

            return planDto;
        }
    }
}
