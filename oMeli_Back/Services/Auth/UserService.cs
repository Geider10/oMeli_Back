using oMeli_Back.Context;
using oMeli_Back.DTOs.Auth;
using oMeli_Back.DTOs;
using Microsoft.EntityFrameworkCore;
using oMeli_Back.Utils;

namespace oMeli_Back.Services.Auth
{
    public class UserService
    {
        private AppDBContext _context;
        private Util _util;
        public UserService(AppDBContext context, Util util)
        {
            _context = context;
            _util = util;
        }

        public async Task<GetUserDto> GetUserById(string userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null) throw new Exception("User not found");
            var userRols = user.UserRoles.Select(u => u.Role.Name).ToList();

            var userDto = new GetUserDto
            {
                Name = user.Name,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Rols = userRols
            };

            return userDto;
        }
        public async Task<GeneralRes> UpdateUser(string userId, UpdateUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null) throw new Exception("User not found");

            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Phone = userDto.Phone;
            user.Email = userDto.Email;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "User updated" };
        }

        public async Task<GeneralRes> UpdatePassword(string userId, UpdatePasswordDto passwordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            if (user == null) throw new Exception("User not found");

            var validatePassword = _util.VerifyHashText(passwordDto.CurrentPassword, user.Password);
            if (!validatePassword) throw new Exception("Password not match");

            var hashPassword = _util.HastText(passwordDto.NewPassword);
            user.Password = hashPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "Password udpated" };
        }
    }
}
