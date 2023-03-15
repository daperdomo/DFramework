using AutoMapper;
using DFramework.Application.Common.Interfaces;
using DFramework.Contracts.Localization;
using MediatR;
using Microsoft.Extensions.Localization;

namespace DFramework.Application.Localization.Queries.GetLocalization
{
    public class GetLocalizationHandler : IRequestHandler<GetLocalizationQuery, IEnumerable<LocalizedKeyDto>>
    {
        private readonly IStringLocalizer _localizer;
        private readonly IMapper _mapper;

        public GetLocalizationHandler(IStringLocalizer localizer, IMapper mapper)
        {
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocalizedKeyDto>> Handle(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            var resources = await Task.Run(() => _localizer.GetAllStrings());

            return _mapper.Map<IEnumerable<LocalizedKeyDto>>(resources);
        }
    }
}
