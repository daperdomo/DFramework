using DFramework.Contracts.Authentication;

namespace DFramework.Application.Common.Interfaces.Authentication
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AuthenticateResponse request);
    }
}
