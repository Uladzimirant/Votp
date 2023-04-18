using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Votp.Contracts;
using Votp.Contracts.Services;

namespace Votp.Services
{
    public class TokenCheckerService : ITokenCheckerService
    {
        private ILogger<TokenCheckerService> _logger;
        private ITokenService _tokenService;

        public TokenCheckerService(ILogger<TokenCheckerService> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<bool> Check(string user, string code)
        {
            var tokens = (await _tokenService.GetTokens()).FindAll(o => o.UserName == user);
            if (tokens.Count == 0) { return false; }

            foreach (var token in tokens)
            {
                if (token.Check(code)) return true;
            }
            return false;
        }
    }
}
