using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Caching;
using DFramework.Application.Common.Interfaces.Services;
using DFramework.Contracts.Configuration;
using Newtonsoft.Json;

namespace DFramework.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        public SettingService(IDFrameworkDbContext dbContext, ICacheManager cacheManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SettingKeyDto>> GetAllKeys()
        {
            return await _cacheManager.GetAsync("setting-get-all", () =>
            {
                return _dbContext.Settings.ToList().Select(m => _mapper.Map<SettingKeyDto>(m));
            });
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var keys = await GetAllKeys();

            var value = keys.FirstOrDefault(m => m.Name == key);
            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value.Value)!;
            }
            return default(T)!;
        }

        public async Task<T> GetAsync<T>(string key, T defaultValue)
        {
            var keys = await GetAllKeys();

            var value = keys.FirstOrDefault(m => m.Name == key);
            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value.Value)!;
            }
            return defaultValue;
        }

        public async Task<bool> IsSetAsync(string key, string value)
        {
            var keys = await GetAllKeys();

            var currentKey = keys.FirstOrDefault(m => m.Name == key);
            if (currentKey != null)
            {
                return currentKey.Value == value;
            }
            return false;
        }

        public Task SetAsync(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
