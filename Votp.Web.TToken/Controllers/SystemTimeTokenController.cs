using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Votp.Contracts.Services;
using Votp.DS.TToken;
using Votp.Web.TokenBase.Controllers;
using Votp.Web.TToken.Models;

namespace Votp.Web.TToken.Controllers
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
            await AddToken(M.Map<TimeToken>(dto));
            return RedirectToAction("Details", new { id = dto.Value });
        }

        [HttpGet]
        public IActionResult Details(string id) 
        {
            return View();
        }
    }
}
