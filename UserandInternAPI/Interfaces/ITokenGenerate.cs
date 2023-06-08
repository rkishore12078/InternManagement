using UserandInternAPI.Models.DTOs;

namespace UserandInternAPI.Interfaces
{
    public interface ITokenGenerate
    {
        public string GenerateToken(UserDTO user);
    }
}
