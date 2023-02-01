using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Votp.DS.Database.Entities;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services.Contracts.UserResolver;

namespace Votp.WebPage.Admin.System.Controllers
{
    public class SystemController : Controller
    {
        private readonly ILogger<SystemController> _l;
        private readonly IMapper _mapper;
        private readonly IUserResolverService _userResolverService;
        public SystemController(ILogger<SystemController> l, IMapper mapper, IUserResolverService resolverService)
        {
            _l = l;
            _mapper = mapper;
            _userResolverService = resolverService;
        }

        public IActionResult Index()
        {
            return View(_userResolverService.Resolvers.Select(
                (resolver, i) => new UserResolverODto { Id = i, Type = resolver.ToString() ?? "Null" })); ;
        }
        public IActionResult ResolverAdd() {
            return View();
        }

        [HttpPost]
        public IActionResult ResolverAdd(UserResolverIDto dto) {
            _userResolverService.AddResolver(_mapper.Map<ResolverInfo>(dto));
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResolverEdit(int id) {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResolverDelete(int id) {
            _userResolverService.Resolvers.Remove(
                _userResolverService.Resolvers.Select((r, i) => new { resolver = r, i = i }).Where(o => o.i == id).Single().resolver);
            return RedirectToAction(nameof(Index));
        }
    }
}
