using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Services
{
    public interface IPlayerSaveService : IService
    {
        void AddSaveRecord(BaseRecord record);
        List<BaseRecord> RecordsForSaving { get; }
        
        Task<T> GetSavedData<T>(string saveId);

        Task<string> GetSavedJson(string saveId);

        Task SaveData<T>(T save, string saveId);
    }
}