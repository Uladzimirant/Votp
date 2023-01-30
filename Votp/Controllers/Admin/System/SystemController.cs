using Microsoft.AspNetCore.Mvc;
using Votp.ATest;
using Votp.Models.Request;
using Votp.Models.Response;

namespace Votp.WebPage.Admin.System.Controllers
{
    public class SystemController : Controller
    {
        private readonly ILogger<SystemController> _l;
        private readonly IUserResolverService _userResolverService;
        public SystemController(ILogger<SystemController> l, IUserResolverService resolverService)
        {
            _l = l;
            _userResolverService = resolverService;
        }

        public IActionResult Index()
        {
            return View(_userResolverService.Resolvers.Select(
                (resolver, i) => new UserResolverODto { Id = i, Name = resolver.ToString() ?? "Null" })); ;
        }
        public IActionResult ResolverAdd() {
            return View();
        }

        [HttpPost]
        public IActionResult ResolverAdd(UserResolverIDto dto) {
            _userResolverService.Add(dto.Name);
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
