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
