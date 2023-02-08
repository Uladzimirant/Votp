using Microsoft.EntityFrameworkCore;
using Votp.DS.Database;
using Votp.Services.Contracts;

namespace Votp.Services.Realizations
{
    public class TokenCheckerService : ITokenCheckerService
    {
        private ILogger<TokenCheckerService> _l;
        private IVotpDbContext _db;

        public TokenCheckerService(ILogger<TokenCheckerService> logger, IVotpDbContext db)
        {
            _l = logger;
            _db = db;
        }

        public async Task<bool> Check(string user, string token)
        {
            bool res = await _db.Tokens.Include(o => o.User).Where(o => o.Value == token && o.User.Login == user).CountAsync() > 0;
            _l.LogInformation($"User '{user}' validation - {res}");
            return res;
        }
    }
}
