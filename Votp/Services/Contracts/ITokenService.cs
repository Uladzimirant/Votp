using Votp.Models.Request;
using Votp.Models.Response;

namespace Votp.Services.Contracts
{
    public interface ITokenService
    {
        public Task<List<TokenODto>> GetTokens();
        public Task DeleteTokens(IEnumerable<int> ids);
        public Task DisableTokens(IEnumerable<int> ids);
        public Task EnableTokens(IEnumerable<int> ids);
        public Task DeleteTokens(params int[] ids) => DeleteTokens(ids.ToList());
        public Task DisableTokens(params int[] ids) => DisableTokens(ids.ToList());
        public Task EnableTokens(params int[] ids) => EnableTokens(ids.ToList());
        public Task CreateToken(TokenIDto dto);
    }
}
