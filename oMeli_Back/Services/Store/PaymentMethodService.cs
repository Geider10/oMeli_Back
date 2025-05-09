using oMeli_Back.Context;
using oMeli_Back.DTOs.Store;
using oMeli_Back.DTOs;
using oMeli_Back.Entities;
using Microsoft.EntityFrameworkCore;

namespace oMeli_Back.Services.Store
{
    public class PaymentMethodService
    {
        private AppDBContext _context;
        public PaymentMethodService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<GeneralRes> CreatePaymentMethod(CreatePaymentMethodDto paymentMethodDto)
        {
            var pmName = await _context.PaymentMethods.AnyAsync(pm => pm.StoreId == Guid.Parse(paymentMethodDto.StoreId) && pm.Name == paymentMethodDto.Name);
            if (pmName) throw new Exception("Payment method already exists with this name");

            var paymentMethod = new PaymentMethodEntity
            {
                StoreId = Guid.Parse(paymentMethodDto.StoreId),
                Name = paymentMethodDto.Name,
                Type = paymentMethodDto.Type,
            };

            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Payment method created" };
        }

        public async Task<GeneralRes> UpdatePaymentMethod(string pmId, UpdatePaymentMethodDto paymentMethodDto)
        {
            var pmExists = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == Guid.Parse(pmId));
            if(pmExists == null) throw new Exception("Payment method not found");

            bool pmName = await _context.PaymentMethods.AnyAsync(pm => pm.StoreId == pmExists.StoreId && pm.Name == paymentMethodDto.Name);
            if(pmName) throw new Exception("PM already exists with this name");

            pmExists.Name = paymentMethodDto.Name;
            pmExists.Type = paymentMethodDto.Type;

            _context.PaymentMethods.Update(pmExists);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Payment method updated" };
        }

        public async Task<GeneralRes> DeletePaymentMethod(string pmId)
        {
            var pmExist = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == Guid.Parse(pmId));
            if(pmExist == null) throw new Exception("Payment method not found");

            _context.PaymentMethods.Remove(pmExist);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Payment method deleted" };
        }

        public async Task<List<GetPMByStoreDto>> GetPaymentMethodsByStore(string storeId)
        {
            var paymentMethods = await _context.PaymentMethods
                .Where(pm => pm.StoreId == Guid.Parse(storeId))
                .Select(pm => new GetPMByStoreDto
                {
                    PaymentMethodId = pm.Id.ToString(),
                    Name = pm.Name,
                    Type = pm.Type
                }).ToListAsync();
            if (paymentMethods == null || paymentMethods.Count == 0) throw new Exception("No payment methods found");

            return paymentMethods;
        }
    }
}
