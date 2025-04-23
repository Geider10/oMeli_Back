using oMeli_Back.Entities;
using oMeli_Back.Context;
using oMeli_Back.DTOs.Subscription;

namespace oMeli_Back.Services
{
    public class SubscriptionService
    {
        private AppDBContext _context;
        public SubscriptionService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<String> Create(CreateDto createDto)
        {
            Subscription subscription = new Subscription
            {
                UserId = Guid.Parse(createDto.UserId),
                PlanId = Guid.Parse(createDto.PlanId),
                DateStart = createDto.DateStart,
                DateEnd = createDto.DateEnd,
                State = createDto.State,
                Renovation = createDto.Renovation,
            };

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return "Subscription created";
        }
        //actualizar() => cambiar el plan, renovar el plan, desactivar susbcription
        public async Task<string> Update(UpdateDto updateDto)
        {
            var subscription = await _context.Subscriptions.FindAsync(Guid.Parse(updateDto.SubscriptionId));
            if (subscription == null) throw new Exception("Subscription not found");

            subscription.PlanId = Guid.Parse(updateDto.PlanId);
            subscription.DateStart = updateDto.DataStart;
            subscription.DateEnd = updateDto.DateEnd;
            subscription.State = updateDto.State;
            subscription.Renovation = updateDto.Renovation;

            _context.Update(subscription);
            await _context.SaveChangesAsync();

            return "Subscription updated";
        }
            
    }
}
