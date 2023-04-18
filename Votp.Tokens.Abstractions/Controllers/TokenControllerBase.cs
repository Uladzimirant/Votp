using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Votp.Contracts.Services;
using Votp.DS.Entities;

namespace Votp.Tokens.Abstractions.Controllers
{
    public abstract class TokenControllerBase : Controller
    {
        protected readonly ILogger L;
        protected readonly IMapper Mapper;
        protected readonly ITokenService TokenService;
        public TokenControllerBase(ILogger<TokenControllerBase> logger, IMapper mapper, ITokenService tokenService) { 
            L = logger;
            Mapper = mapper;
            TokenService = tokenService;
        }

        protected async Task AddToken(Token token)
        {
            await TokenService.CreateToken(token);
        }

        protected IActionResult NoToken(int id) => ViewErrorTokenMessage($"No token found by id {id}");

        protected IActionResult WrongToken(Type actual, Type expected) => ViewErrorTokenMessage($"Wrong type of token {actual.Name}, expected {expected.Name}");

        protected IActionResult ViewErrorTokenMessage(string message)
        {
            return View("/Tokens/TokenErrorMessage", message);
        }

        protected async Task<(T? token, IActionResult? action)> TryGetTokenByIdAs<T>(int id) where T : class
        {
            var rawToken = (await TokenService.GetTokens()).Single(e => e.Id == id);
            if (rawToken == null) { return (null, NoToken(id)); }
            var token = rawToken as T;
            if (token == null) { return (null, WrongToken(rawToken.GetType(), typeof(T))); }
            return (token, null);
        }
    }
}