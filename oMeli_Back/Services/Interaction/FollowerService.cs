using Microsoft.EntityFrameworkCore;
using oMeli_Back.Context;
using oMeli_Back.DTOs;
using oMeli_Back.DTOs.Interaction;
using oMeli_Back.Entities;
namespace oMeli_Back.Services.Interaction
{
    public class FollowerService
    {
        private AppDBContext _context;
        public FollowerService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<GeneralRes> CreateFollower (CreateFollowerDto followerDto)
        {
            var repiteFollower = await _context.Followers
                .FirstOrDefaultAsync(f => f.StoreId == Guid.Parse(followerDto.StoreId) && f.UserId == Guid.Parse(followerDto.UserId));
            if (repiteFollower != null) throw new Exception("Already exists a user following this store");

            var follower = new FollowerEntity
            {
                StoreId = Guid.Parse(followerDto.StoreId),
                UserId = Guid.Parse(followerDto.UserId)
            };

            await _context.Followers.AddAsync(follower);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Follower created" };
        } 

        public async Task<GeneralRes> DeleteFollower (string storeId, string userId)
        {
            var follower = await _context.Followers
                .FirstOrDefaultAsync(f => f.StoreId == Guid.Parse(storeId) && f.UserId == Guid.Parse(userId));
            if(follower == null) throw new Exception("Follower not found");

            _context.Followers.Remove(follower);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Follower deleted" };
        }

        public async Task<GetFollowersByStoreDto> GetFollowersByStore(string storeId)
        {
            var storeExists = await _context.Stores.FirstOrDefaultAsync(s => s.Id == Guid.Parse(storeId));
            if(storeExists == null) throw new Exception("Store not found");

            var countFollowers =  _context.Followers.Where(f => f.StoreId == Guid.Parse(storeId)).Count();
            return new GetFollowersByStoreDto
            {
                Ok = true,
                Message = "Get followers quantity",
                Count = countFollowers
            };
        }

        public async Task<List<GetStoresFollowedByUserDto>> GetStoresFollowedByUser(string userId)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (userExists == null) throw new Exception("User not found");

            var storesFollowed = await _context.Followers
                .Where(f => f.UserId == Guid.Parse(userId))
                .Select(f => new GetStoresFollowedByUserDto
                {
                    StoreId = f.StoreId.ToString()
                })
                .ToListAsync();
            

            return storesFollowed;
        }   
    }
}
