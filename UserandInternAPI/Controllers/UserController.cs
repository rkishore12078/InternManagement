using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserandInternAPI.Exceptions;
using UserandInternAPI.Models.DTOs;
using UserandInternAPI.Services;

namespace UserandInternAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices=userServices;
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register(InternDTO intern)
        {
            try
            {
                var result = await _userServices.Register(intern);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Unable to register at this moment");
            }
            catch (InvalidSqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
