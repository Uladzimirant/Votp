using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Votp.Contracts;
using Votp.Contracts.Services;

namespace Votp.Services
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

        public async Task<bool> Check(string user, string code)
        {
            bool res = (await _db.Tokens.Include(o => o.User).Where(o => o.User.Login == user).SingleAsync()).Check(code);
            _l.LogInformation($"User '{user}' validation - {res}");
            return res;
        }
    }
}
