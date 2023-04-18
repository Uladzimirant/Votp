using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Votp.Contracts;
using Votp.Contracts.Services;
using Votp.DS.Entities;
using Votp.Exceptions;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services;

namespace Votp.Controllers.Admin
{
    public class UsersController : Controller
    {
        private ILogger<UsersController> _l;
        private readonly IMapper _m;
        private IUserService _userService;
        private IInnerUsersDBContext _innerUsersDB;
        public UsersController(ILogger<UsersController> l, IMapper mapper, IUserService userService, IInnerUsersDBContext innerUsersDB)
        {
            _l = l;
            _m = mapper;
            _userService = userService;
            _innerUsersDB = innerUsersDB;
        }

        public async Task<IActionResult> InnerUsers()
        {
            return View((await _innerUsersDB.Users.ToListAsync()).Select(_m.Map<UserODto>));
        }

        public async Task<IActionResult> Index()
        {
            return View((await _userService.GetUsers()).Select(_m.Map<UserODto>));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserIDto dto)
        {
            try
            {
                if ((await _userService.GetUsers()).Count(u => u.Login == dto.Name) > 0) throw new ExpectedException($"User with name '{dto.Name}' already exists");
                _innerUsersDB.Users.Add(new User() { Login = dto.Name });
                await _innerUsersDB.AsContext().SaveChangesAsync();
                return RedirectToAction(nameof(InnerUsers));
            }
            catch (ExpectedException ex) { 
                return View("ErrorMessage",ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SelectionAction([FromForm] SelectionIDto sel)
        {
            switch (sel.Action)
            {
                case "Delete":
                    await _innerUsersDB.Users.Where(u => sel.Selection.Contains(u.Id)).ExecuteDeleteAsync();
                    break;
                default:
                    throw new ArgumentException($"Unsupported button value: {sel.Action}");
            }
            return RedirectToAction(nameof(InnerUsers));
        }


    }
}
