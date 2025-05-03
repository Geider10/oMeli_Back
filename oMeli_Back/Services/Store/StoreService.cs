using oMeli_Back.Context;
using oMeli_Back.DTOs;
using oMeli_Back.DTOs.Store;
using oMeli_Back.Entities;

namespace oMeli_Back.Services.Store
{
    public class StoreService
    {
        private AppDBContext _context;
        public StoreService(AppDBContext context)
        {
            _context = context;           
        }

        public async Task<GeneralRes> CreateStore(CreateStoreDto storeDto)
        {
            var store = new StoreEntity
            {
                UserId = Guid.Parse(storeDto.UserId),
                SubscriptionId = Guid.Parse(storeDto.SubscriptionId),
                Name = storeDto.Name,
                Wassap = storeDto.Wassap,
                Mail = storeDto.Mail,
                HasLocal = storeDto.HasLocal,
                Address = storeDto.Address,
                AddressDescription = storeDto.AddressDescription,
                LocalNumber = storeDto.LocalNumber,
                CurrentProducts = storeDto.CurrentProducts
            };
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Store created" };
        }

    }
}
