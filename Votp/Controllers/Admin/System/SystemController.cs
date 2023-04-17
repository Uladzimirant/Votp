using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Entities;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.UserResolver.InnerDatabase;
using Votp.UserResolver.Ldap;

namespace Votp.Web.Admin.System.Controllers
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

        public async Task<IActionResult> Index()
        {
            return View((await _userResolverService.GetResolverInfos()).Select(
                (resolver) =>
                {
                    var dto = _mapper.Map<UserResolverODto>(resolver);
                    return dto;
                })); 
        }
        public IActionResult ResolverAdd()
        {
            return View();
        }
        public IActionResult ResolverAddLdap() {
            return View();
        }

        [HttpPost]
        public IActionResult ResolverAdd(UserResolverDatabaseIDto dto) {
            if (_userResolverService.Resolvers.Any(e => e is DatabaseUserResolver)) return RedirectToAction(nameof(Index));
            _userResolverService.AddResolver(_mapper.Map<DatabaseUserResolverInfo>(dto));
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ResolverAddLdap(UserResolverLdapIDto dto)
        {
            var info = _mapper.Map<LdapUserResolverInfo>(dto);
            _userResolverService.AddResolver(info);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResolverEdit(int id) {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResolverDelete(int id) {
            _userResolverService.RemoveResolver(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
