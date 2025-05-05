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
            var storeTypePMs = await _context.PaymentMethods.Where(pm => pm.StoreId == Guid.Parse(paymentMethodDto.StoreId) && pm.Type == paymentMethodDto.Type).ToListAsync();
            var repiteName = storeTypePMs.Find(pm => pm.Name == paymentMethodDto.Name);
            if (repiteName != null) throw new Exception("PM already exists with this name");

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
            var storeTypePMs = await _context.PaymentMethods.Where(pm=> pm.StoreId == pmExists.StoreId && pm.Type == paymentMethodDto.Type).ToListAsync();
            var repiteName = storeTypePMs.Find(pm => pm.Name == paymentMethodDto.Name);
            if(repiteName != null) throw new Exception("PM already exists with this name");

            pmExists.Name = paymentMethodDto.Name;
            pmExists.Type = paymentMethodDto.Type;

            _context.PaymentMethods.Update(pmExists);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Payment method updated" };
        }
    }
}
