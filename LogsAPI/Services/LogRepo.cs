using LogsAPI.Exceptions;
using LogsAPI.Interfaces;
using LogsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LogsAPI.Services
{
    public class LogRepo:Ilogs
    {
        private readonly LogContext _context;

        public LogRepo(LogContext context)
        {
            _context = context;
        }

        public async Task<logs?> Add(logs log)
        {
            try
            {
                _context.Logs.Add(log);
                await _context.SaveChangesAsync();
                return log;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }

        public Task<logs?> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<List<logs>?> GetAll()
        {
            try
            {
                var logs = await _context.Logs.ToListAsync();
                if (logs != null)
                    return logs;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<logs?> GetLogs(int key)
        {
            try
            {
                var log = await _context.Logs.SingleOrDefaultAsync(l => l.LogID == key);
                if (log != null)
                    return log;
            }
            catch (InvalidSqlException ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
            return null;
        }

        public async Task<logs?> Update(logs log)
        {
            var myLog = await GetLogs(log.LogID);
            if (myLog != null)
            {
                myLog.LogOut = DateTime.Now;
                _context.Logs.Update(myLog);
                await _context.SaveChangesAsync();
                return myLog;
            }
            return null;
        }
    }
}
