using Microsoft.EntityFrameworkCore;
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
            var repiteStore = await _context.Stores
                .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(storeDto.UserId) && s.SubscriptionId == Guid.Parse(storeDto.SubscriptionId));
            if (repiteStore != null) throw new Exception("store already exists to this user");

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
                CurrentProducts = 0
            };
            var sellerRol = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Seller");
            var userRole = new UserRoleEntity
            {
                UserId = Guid.Parse(storeDto.UserId),
                RoleId = sellerRol.Id
            };

            await _context.Stores.AddAsync(store);
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Store created" };
        }
        
        public async Task<GeneralRes> UpdateStore (UpdateStoreDto storeDto, string storeId)
        {
            var storeExists = await _context.Stores.FirstOrDefaultAsync(s => s.Id == Guid.Parse(storeId));
            if (storeExists == null) throw new Exception("store not found");

            storeExists.Name = storeDto.Name;
            storeExists.Wassap = storeDto.Wassap;
            storeExists.Mail = storeDto.Mail;
            storeExists.HasLocal = storeDto.HasLocal;
            storeExists.Address = storeDto.Address;
            storeExists.AddressDescription = storeDto.AddressDescription;
            storeExists.LocalNumber = storeDto.LocalNumber;

            _context.Stores.Update(storeExists);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Store updated" };
        }

        public async Task<GetStoreByUserDto> GetStoreByUser( string userId)
        {
            var storeExists = await _context.Stores.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
            if (storeExists == null) throw new Exception("store not found");

            var store = new GetStoreByUserDto
            {
                StoreId = storeExists.Id.ToString(),
                Name = storeExists.Name,
                Wassap = storeExists.Wassap,
                Mail = storeExists.Mail,
                HasLocal = storeExists.HasLocal,
                Address = storeExists.Address,
                AddressDescription = storeExists.AddressDescription,
                LocalNumber = storeExists.LocalNumber
            };

            return store;
        }
    }
}
