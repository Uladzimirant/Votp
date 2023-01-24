using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services.Contracts;
using Votp.Utils;

namespace Votp.Services.Realizations
{
    public class PlaceholderTokenService : ITokenService
    {
        private List<TokenODto> _tokens;
        private List<UserODto> _users;
        public PlaceholderTokenService()
        {
            var r = Randomizer.Instance;
            _tokens = r.GenerateSequence(3, 6, i => new TokenODto() { Token = r.NextAlphaNum(10), UserName = r.NextWord(5) }).ToList();
            _users = r.GenerateSequence(3, 6, i => new UserODto() { Name = r.NextWord(5), Email = r.NextWord(5) + "@mail.ru" }).ToList();
        }

        public void CreateToken(TokenIDto dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteTokens(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void DisableTokens(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void EnableTokens(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public List<TokenODto> GetTokens()
        {
            return _tokens;
        }

        public List<UserODto> GetUsers()
        {
            return _users;
        }
    }
}
