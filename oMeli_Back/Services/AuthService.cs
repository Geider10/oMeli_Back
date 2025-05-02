using oMeli_Back.Context;
using oMeli_Back.Utils;
using Microsoft.EntityFrameworkCore;
using oMeli_Back.Entities;
using oMeli_Back.DTOs.Auth;
using oMeli_Back.DTOs;

namespace oMeli_Back.Services
{
    public class AuthService
    {
        private readonly AppDBContext _context;
        private readonly Util _util;
        public AuthService(AppDBContext context, Util util)
        {
            _context = context;
            _util = util;
        }

        public async Task<GeneralRes> SignUp(SignUpDto signUpDto)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == signUpDto.Email);
            if (userExist != null) throw new Exception("User already exists");

            string hashedPassword = _util.HastText(signUpDto.Password);
            UserEntity user = new UserEntity
            {
                Name = signUpDto.Name,
                LastName = signUpDto.LastName,
                Phone = signUpDto.Phone,
                Email = signUpDto.Email,
                Password = hashedPassword
            };
            var buyerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Buyer");
            if (buyerRole == null) throw new Exception("Buyer Role not found");

            UserRoleEntity userRole = new UserRoleEntity
            {
                UserId = user.Id,
                RoleId = buyerRole.Id
            };
            //save all
            await _context.Users.AddAsync(user);
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return new GeneralRes { Ok = true, Message = "User created" };
        }

        public async Task<String> LogIn(LogInDto logInDto)
        {
            var userExists = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == logInDto.Email);
            if (userExists == null) throw new Exception("User not found");
            var userRoles = userExists.UserRoles.Select(ur => ur.Role.Name).ToList();

            bool verifyPassword = _util.VerifyHashText(logInDto.Password, userExists.Password);
            if (!verifyPassword) throw new Exception("Invalid password");

            string token = _util.GenerateToken(userExists.Id.ToString(), userRoles);

            return token;
        }
    }
}
