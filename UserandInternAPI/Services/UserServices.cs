using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UserandInternAPI.Interfaces;
using UserandInternAPI.Models;
using UserandInternAPI.Models.DTOs;

namespace UserandInternAPI.Services
{
    public class UserServices
    {
        private IRepo<int, User> _userRepo;
        private IRepo<int, Intern> _internRepo;
        private ITokenGenerate _tokenService;
        private IPasswordGenerate _passwordService;

        public UserServices(IRepo<int,User> userRepo, IRepo<int, Intern> internRepo,
                            ITokenGenerate tokenService,IPasswordGenerate passwordService)
        {
            _userRepo=userRepo;
            _internRepo=internRepo;
            _tokenService=tokenService;
            _passwordService = passwordService;
        }
        public async Task<UserDTO> ChangeStatus(UserDTO userDTO)
        {
            var users = await _userRepo.GetAll();
            var user = users.SingleOrDefault(u=>u.UserId==userDTO.UserId && u.Role==userDTO.Role);
            var updatedUser = await _userRepo.Update(user);
            return userDTO;
        }

        public async Task<UserDTO> Login(UserDTO user)
        {
            var userData = await _userRepo.Get(user.UserId);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.PasswordKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.PasswordHash[i])
                        return null;
                }
                user = new UserDTO();
                user.UserId = userData.UserId;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }
        public async Task<UserDTO> Register(InternDTO intern)
        {

            UserDTO user = null;
            var hmac = new HMACSHA512();
            string? generatedPassword = await _passwordService.GeneratePassword(intern);
            intern.User.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(generatedPassword ?? "1234"));
            intern.User.PasswordKey = hmac.Key;
            intern.User.Role = "Admin";
            intern.User.Status = "Approved";
            var userResult = await _userRepo.Add(intern.User);
            var internResult = await _internRepo.Add(intern);
            if (userResult != null && internResult != null)
            {
                user = new UserDTO();
                user.UserId = internResult.Id;
                user.Role = userResult.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;

        }

        public async Task<ChangePasswordDTO> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var user = await _userRepo.Get(passwordDTO.UserID);
            if (user != null)
            {
                var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordDTO.NewPassword));
                user.PasswordKey = hmac.Key;
                var result = await _userRepo.Update(user);
                if (result != null)
                {
                    return passwordDTO;
                }
            }
            return null;
        }
    }
}
