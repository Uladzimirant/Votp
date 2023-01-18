using Votp.Models.Response;

namespace Votp.Services.Contracts
{
    public interface ITokenService
    {
        public List<TokenODto> GetTokens();
        public List<UserODto> GetUsers();
    }
}
