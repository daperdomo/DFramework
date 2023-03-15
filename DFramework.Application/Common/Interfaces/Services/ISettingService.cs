using DFramework.Contracts.Configuration;

namespace DFramework.Application.Common.Interfaces.Services
{
    public interface ISettingService
    {
        Task<T> GetAsync<T>(string key);
        Task<bool> IsSetAsync(string key, string value);
        Task<T> GetAsync<T>(string key, T defaultValue);
        Task SetAsync(string key, string value);
        Task<IEnumerable<SettingKeyDto>> GetAllKeys();
    }
}