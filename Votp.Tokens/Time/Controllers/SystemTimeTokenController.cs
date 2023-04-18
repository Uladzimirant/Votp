using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Votp.Contracts.Services;
using Votp.Tokens.Abstractions.Controllers;
using Votp.Tokens.Abstractions.Models;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Time.Models;
using Votp.Tokens.Totp.Entities;

namespace Votp.Tokens.Time.Controllers
{
    [Route("Tokens/[action]/[controller]")]
    public class SystemTimeTokenController : TokenControllerBase
    {
        public SystemTimeTokenController(ILogger<SystemTimeTokenController> logger, IMapper mapper, ITokenService service)
            : base(logger, mapper, service) { }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TimeTokenIDto dto)
        {
            await AddToken(Mapper.Map<TimeToken>(dto));
            return RedirectToAction("Details", new { id = (await TokenService.GetTokens()).Single(t => t.Name == dto.Name).Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            var (token, action) = await TryGetTokenByIdAs<TimeToken>(id);
            if (token == null) { return action ?? throw new NullReferenceException("TryGetTokenByIdAs returned second null in action which not supposed to happen"); }

            return View(Mapper.Map<TokenODto>(token));
        }
    }
}
