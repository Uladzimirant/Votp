using Microsoft.AspNetCore.Mvc;
using Votp.ATest;

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
            return View(_userResolverService.Resolvers);
        }
    }
}
