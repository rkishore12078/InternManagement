using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserandInternAPI.Exceptions;
using UserandInternAPI.Interfaces;
using UserandInternAPI.Models;

namespace UserandInternAPI.Services
{
    public class InternRepo : IRepo<int, Intern>
    {
        private readonly UserContext _context;

        public InternRepo(UserContext context)
        {
            _context = context;
        }
        public async Task<Intern?> Add(Intern item)
        {
            try
            {
                _context.Interns.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (SqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<Intern?> Delete(int key)
        {
            var intern = await Get(key);
            if (intern != null)
            {
                _context.Interns.Remove(intern);
                await _context.SaveChangesAsync();
                return intern;
            }
            return null;
        }

        public async Task<Intern?> Get(int key)
        {
            var intern = await _context.Interns.FirstOrDefaultAsync(i => i.Id == key);
            return intern;
        }

        public async Task<ICollection<Intern>?> GetAll()
        {
            var interns = await _context.Interns.ToListAsync();
            if (interns.Count > 0)
                return interns;
            return null;
        }

        public async Task<Intern?> Update(Intern item)
        {
            var intern = await Get(item.Id);
            if (intern != null)
            {
                intern.Age = item.Age>0?item.Age:intern.Age;
                intern.User = item.User!=null?item.User:intern.User;
                intern.Email = item.Email!=null?item.Email:intern.Email;
                intern.Phone = item.Phone != null ? item.Phone : intern.Phone;
                intern.DateOfBirth = item.DateOfBirth;
                await _context.SaveChangesAsync();
            }
            return intern;
        }
    }
}
