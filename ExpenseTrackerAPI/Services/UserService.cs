using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.Interfaces;

namespace ExpenseTrackerAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ExpenseTrackerContext _context;
        private readonly IJwtUtils _jwtUtils;

        public UserService(ExpenseTrackerContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            // Authentication successful
            return user;
        }

        public async Task<User> Register(string username, string password)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User GetById(int id) => _context.Users.Find(id);
    }
}