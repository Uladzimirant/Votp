using Votp.Models.Request;
using Votp.Models.Response;

namespace Votp.Services.Contracts
{
    public interface ITokenService
    {
        public List<TokenODto> GetTokens();
        public void DeleteTokens(IEnumerable<int> ids);
        public void DisableTokens(IEnumerable<int> ids);
        public void EnableTokens(IEnumerable<int> ids);
        public void DeleteTokens(params int[] ids) => DeleteTokens(ids.ToList());
        public void DisableTokens(params int[] ids) => DisableTokens(ids.ToList());
        public void EnableTokens(params int[] ids) => EnableTokens(ids.ToList());
        public void CreateToken(TokenIDto dto);
    }
}
