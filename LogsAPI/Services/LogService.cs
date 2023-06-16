using LogsAPI.Interfaces;
using LogsAPI.Models;

namespace LogsAPI.Services
{
    public class LogService:IManageLogs
    {
        private readonly Ilogs _logRepo;

        public LogService(Ilogs logRepo)
        {
            _logRepo = logRepo;
        }

        public async Task<logs?> Add(logs log)
        {
            var myLog = await _logRepo.GetLogs(log.LogID);
            if (myLog != null) return null;
            log.LogOut = default(DateTime);
            var result = await _logRepo.Add(log);
            if (result != null)
                return log;
            return null;
        }

        public async Task<List<logs>?> GetAll()
        {
            return await _logRepo.GetAll();
        }

        public async Task<List<logs>?> LogByDate(DateTime date)
        {
            var logs = await _logRepo.GetAll();
            if (logs == null) return null;
            var logsByDate = logs.Where(l => l.LogOut.Date == date).ToList();
            if (logsByDate != null)
                return logsByDate;
            return null;
        }

        public async Task<List<logs>?> LogByIntern(int key)
        {
            var logs = await _logRepo.GetAll();
            if (logs == null) return null;
            var logsByIntern = logs.Where(l => l.InterId == key).ToList();
            if (logsByIntern != null)
                return logsByIntern;
            return null;
        }

        public async Task<logs?> Update(logs log)
        {
            var myLog = await _logRepo.Update(log);
            if (myLog != null) return myLog;
            return null;
        }
    }
}
