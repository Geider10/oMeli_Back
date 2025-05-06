using Microsoft.EntityFrameworkCore;
using oMeli_Back.Context;
using oMeli_Back.DTOs;
using oMeli_Back.DTOs.Store;
using oMeli_Back.Entities;
namespace oMeli_Back.Services.Store
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
            var countFollowers =  _context.Followers.Where(f => f.StoreId == Guid.Parse(storeId)).Count();
            if (countFollowers == 0) throw new Exception("No followers for this store");

            return new GetFollowersByStoreDto
            {
                Ok = true,
                Message = "Get followers quantity",
                Count = countFollowers
            };
        }

        public async Task<List<GetStoreFollowByUserDto>> GetStoresFollowedByUser(string userId)
        {
            var storesFollowed = await _context.Followers.Where(f => f.UserId == Guid.Parse(userId)).ToListAsync();
            if (storesFollowed == null || storesFollowed.Count == 0) throw new Exception("No stores followed by this user");
            var storesMap = storesFollowed.Select(s => new GetStoreFollowByUserDto
            {
                StoreId = s.StoreId.ToString()
            }).ToList();

            return storesMap;
        }   
    }
}
