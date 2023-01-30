using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Votp.Models.Request;
using Votp.Services.Contracts;

namespace Votp.Controllers.Admin
{
    public class UsersController : Controller
    {
        private ILogger<UsersController> _l;
        private IUserService _userService;
        public UsersController(ILogger<UsersController> l, IUserService userService)
        {
            _l = l;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUsers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserIDto dto)
        {
            //Temp method, so 
            var uservice = (Votp.Services.Realizations.DBTokenService)_userService;
            uservice.CreateUser(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SelectionAction([FromForm] SelectionIDto sel)
        {
            var uservice = (Votp.Services.Realizations.DBTokenService)_userService;
            switch (sel.Action)
            {
                case "Delete":
                    uservice.DeleteUsers(sel.Selection);
                    break;
                default:
                    throw new ArgumentException($"Unsupported button value: {sel.Action}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
