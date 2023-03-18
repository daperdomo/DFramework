using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Contracts.Localization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Localization.Queries.GetLocalization
{
    public class GetLocalizationHandler : IRequestHandler<GetLocalizationQuery, Dictionary<string, string>>
    {
        private readonly IStringLocalizer _localizer;
        private readonly IMapper _mapper;

        public GetLocalizationHandler(IStringLocalizer localizer, IMapper mapper)
        {
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Dictionary<string, string>> Handle(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            var resources = await Task.Run(() => _localizer.GetAllStrings());
            var dic = resources.Select(m => new KeyValuePair<string, string>(m.Name, m.Value));
            return new Dictionary<string, string>(dic);
        }
    }
}
