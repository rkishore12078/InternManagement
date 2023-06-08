using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserandInternAPI.Exceptions;
using UserandInternAPI.Interfaces;
using UserandInternAPI.Models;

namespace UserandInternAPI.Services
{
    public class UserRepo : IRepo<int, User>
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }
        public async Task<User?> Add(User item)
        {
            try
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<User?> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<User?> Get(int key)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == key);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<ICollection<User>?> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count > 0)
                return users;
            return null;
        }

        public async Task<User?> Update(User item)
        {
            try
            {
                var user = await Get(item.UserId);
                if (user != null)
                {
                    user.Role = item.Role!=null?item.Role:user.Role;
                    user.PasswordHash = item.PasswordHash != null ? item.PasswordHash : user.PasswordHash;
                    user.PasswordKey = item.PasswordKey != null ? item.PasswordKey : user.PasswordKey;
                    user.Status = "Approved";
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }
    }
}