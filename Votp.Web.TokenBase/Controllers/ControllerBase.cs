using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Votp.Contracts.Services;
using Votp.DS.Database.Entities;

namespace Votp.Web.TokenBase.Controllers
{
    public abstract class TokenControllerBase : Controller
    {
        protected readonly ILogger L;
        protected readonly IMapper M;
        protected readonly ITokenService TokenService;
        public TokenControllerBase(ILogger<TokenControllerBase> logger, IMapper mapper, ITokenService tokenService) { 
            L = logger;
            M = mapper;
            TokenService = tokenService;
        }

        protected async Task AddToken(Token token)
        {
            await TokenService.CreateToken(token);
        }
    }
}