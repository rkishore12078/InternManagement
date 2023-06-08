using UserandInternAPI.Models;

namespace UserandInternAPI.Interfaces
{
    public interface IPasswordGenerate
    {
        public Task<string?> GeneratePassword(Intern intern);
    }
}
