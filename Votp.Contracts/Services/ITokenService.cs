using Votp.DS.Database.Entities;

namespace Votp.Contracts.Services
{
    public interface ITokenService
    {
        public Task<List<Token>> GetTokens();
        public Task DeleteTokens(IEnumerable<int> ids);
        public Task DisableTokens(IEnumerable<int> ids);
        public Task EnableTokens(IEnumerable<int> ids);
        public Task DeleteTokens(params int[] ids) => DeleteTokens(ids.ToList());
        public Task DisableTokens(params int[] ids) => DisableTokens(ids.ToList());
        public Task EnableTokens(params int[] ids) => EnableTokens(ids.ToList());
        public Task CreateToken(Token dto);
    }
}
