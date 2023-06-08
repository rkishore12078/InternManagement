using UserandInternAPI.Interfaces;
using UserandInternAPI.Models;

namespace UserandInternAPI.Services
{
    public class PasswordService : IPasswordGenerate
    {
        public async Task<string?> GeneratePassword(Intern intern)
        {
            string? password;
            password = intern.Name.Substring(0, 4);
            password += intern.DateOfBirth.Date;
            password += intern.DateOfBirth.Month;
            return password;
        }
    }
}
