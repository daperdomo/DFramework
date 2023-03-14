using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Application.Common.Interfaces.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace DFramework.Application.Services.Localization
{
    public class DbStringLocalizer : IStringLocalizer
    {
        private readonly IDFrameworkDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        public DbStringLocalizer(IDFrameworkDbContext dbContext, ICacheManager cacheManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }
        public LocalizedString this[string name] => this[name, null!];

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var allLocalizations = GetAllStrings(true);

                var value = allLocalizations.FirstOrDefault(x => x.Name == name);
                var resourceValue = name;

                if (value != null)
                {
                    resourceValue = arguments != null ? string.Format(value, arguments) : value;
                }

                return new LocalizedString(name, resourceValue);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentCulture.Name;
            return _cacheManager.Get($"localization-{culture}", () =>
            {
                var languageResource = _dbContext.Languages
                .Include(m => m.LanguageResources)
                .FirstOrDefault(m => m.Culture == culture);

                if (languageResource != null)
                {
                    return languageResource.LanguageResources.Select(m => _mapper.Map<LocalizedString>(m));
                }
                return null!;
            });
        }
    }
}
