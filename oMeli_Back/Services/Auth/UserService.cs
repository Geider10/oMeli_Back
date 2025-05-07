using oMeli_Back.Context;
using oMeli_Back.DTOs.Auth;
using oMeli_Back.DTOs;
using Microsoft.EntityFrameworkCore;

namespace oMeli_Back.Services.Auth
{
    public class UserService
    {
        private AppDBContext _context;
        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<GetUserDto> GetUserById(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null) throw new Exception("User not found");

            var userDto = new GetUserDto
            {
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
            };

            return userDto;
        }
    }
}
