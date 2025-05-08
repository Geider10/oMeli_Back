using oMeli_Back.Entities;
using oMeli_Back.Context;
using oMeli_Back.DTOs.Subscription;
using Microsoft.EntityFrameworkCore;
using oMeli_Back.DTOs;
using oMeli_Back.Utils;
namespace oMeli_Back.Services
{
    public class SubscriptionService
    {
        private AppDBContext _context;
        private Util _util;
        public SubscriptionService(AppDBContext context, Util util)
        {
            _context = context;
            _util = util;
        }
        public async Task<GetByUserDto> GetByUser (string userId)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
            if (subscription == null) throw new Exception("Subscription not found");

            var userSubscription = new GetByUserDto
            {
                SubscriptionId = subscription.Id,
                NamePlan = subscription.Plan.Name,
                State = subscription.State,
                Renovation = subscription.Renovation,
                DateStart = subscription.DateStart,
                DateEnd = subscription.DateEnd
            };

            return userSubscription;
        }
        public async Task<GeneralRes> CreateSubscription(CreateSubscriptionDto createDto)
        {
            var repiteSubs = await _context
                .Subscriptions.FirstOrDefaultAsync( s => s.UserId == Guid.Parse(createDto.UserId) && s.PlanId == Guid.Parse(createDto.PlanId));
            if (repiteSubs != null) throw new Exception("Susbcription already exists");

            SubscriptionEntity subscription = new SubscriptionEntity
            {
                UserId = Guid.Parse(createDto.UserId),
                PlanId = Guid.Parse(createDto.PlanId),
                DateStart = _util.ConvertDate(createDto.DateStart),
                DateEnd = _util.ConvertDate(createDto.DateEnd),
                State = createDto.State,
                Renovation = createDto.Renovation,
            };

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Subscription created" };
        }
        //actualizar() => cambiar a otro plan, renovar el plan, desactivar susbcription
        public async Task<GeneralRes> Update(UpdateDto updateDto, string subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(Guid.Parse(subscriptionId));
            if (subscription == null) throw new Exception("Subscription not found");

            subscription.PlanId = Guid.Parse(updateDto.PlanId);
            subscription.DateStart = _util.ConvertDate(updateDto.DataStart);
            subscription.DateEnd = _util.ConvertDate(updateDto.DateEnd);
            subscription.State = updateDto.State;
            subscription.Renovation = updateDto.Renovation;

            _context.Update(subscription);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Subscription updated" };
        }
            
    }
}
