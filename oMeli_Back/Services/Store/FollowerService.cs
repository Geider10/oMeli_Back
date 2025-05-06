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
    }
}
