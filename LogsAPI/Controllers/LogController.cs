using LogsAPI.Exceptions;
using LogsAPI.Interfaces;
using LogsAPI.Models;
using LogsAPI.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AngularCORS")]
    public class LogController : ControllerBase
    {
        private readonly IManageLogs _logService;

        public LogController(IManageLogs logService)
        {
            _logService = logService;
        }
        [HttpPost]
        public async Task<ActionResult<logs?>> Add(logs log)
        {
            try
            {
                int result = DateTime.Compare(log.LogIn, log.LogOut);
                if (result < 0)
                    return BadRequest(new Error(4, "Login date should be less than log out date"));
                var myLog = await _logService.Add(log);
                if (myLog != null)
                    return Created("Log Added Successfully", myLog);
                return BadRequest(new Error(1, "Unable to Add Log"));
            }
            catch (InvalidSqlException ex)
            {
                return BadRequest(new Error(3, ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<logs>?>> LogsByDate(DateTime date)
        {
            var logs = await _logService.LogByDate(date);
            if (logs != null)
                return Ok(logs);
            return BadRequest(new Error(2, "There is No Logs"));
        }
        [HttpPost]
        public async Task<ActionResult<List<logs>?>> LogsByIntern(InternDTO internID)
        {
            var logs = await _logService.LogByIntern(internID.InternId);
            if (logs != null)
                return Ok(logs);
            return BadRequest(new Error(2, "There is No Logs"));
        }

        [HttpPut]
        public async Task<ActionResult<logs>> Update(logs log)
        {
            var myLog = await _logService.Update(log);
            if (myLog != null) return Ok(myLog);
            return BadRequest(new Error(3, "not able to update"));
        }
        [HttpGet]
        public async Task<ActionResult<List<logs>?>> GetAll()
        {
            var logs= await _logService.GetAll();
            if(logs != null)
                return Ok(logs);
            return NotFound("No logs are available");
        }
    }
}
