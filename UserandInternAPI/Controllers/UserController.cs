﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserandInternAPI.Exceptions;
using UserandInternAPI.Models;
using UserandInternAPI.Models.DTOs;
using UserandInternAPI.Services;

namespace UserandInternAPI.Controllers
{
    [Route("api/[controller]/[action]")]
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
                return BadRequest(new Error(1,"Unable to Add User"));
            }
            catch (InvalidSqlException ex)
            {
                return BadRequest(new Error(2,ex.Message));
            }
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> ChangeStatus(UserDTO userDTO)
        {
            var user= await _userServices.ChangeStatus(userDTO);
            if (user != null)
                return Ok("Status successfully changed to Approved");
            return BadRequest(new Error(3,"Unable to change status"));
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> LogIN(UserDTO userDTO)
        {
            var user= await _userServices.Login(userDTO);
            if(user != null)
                return Ok(user);
            return BadRequest(new Error(4,"Unable to LogIN"));
        }

        [HttpPut]
        public async Task<ActionResult<string>> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            var result=await _userServices.ChangePassword(passwordDTO);
            if (result != null)
                return Ok("Password Updated Successfully");
            return BadRequest(new Error(5,"Unable to update Password"));
        }
    }
}
