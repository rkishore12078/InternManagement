using LogAPI.Models;

namespace LogAPI.Interfaces
{
    public interface IManageLogs
    {
        public Task<logs?> Add(logs log);
        public Task<List<logs>?> GetAll();
        public Task<List<logs>?> LogByDate(DateTime date);
        public Task<List<logs>?> LogByIntern(int key);
        public Task<logs?> Update(logs log);
    }
}
