using LogsAPI.Models;

namespace LogsAPI.Interfaces
{
    public interface Ilogs
    {
        public Task<logs?> Add(logs log);
        public Task<logs?> Delete(int key);
        public Task<logs?> GetLogs(int key);
        public Task<logs?> Update(logs log);
        public Task<List<logs>?> GetAll();
    }
}
