﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Votp.Contracts;
using Votp.Contracts.Services;
using Votp.DS.Entities;
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
        public UsersController(ILogger<UsersController> l, IMapper mapper, IUserService userService)
        {
            _l = l;
            _m = mapper;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _userService.GetUsers()).Select(_m.Map<UserODto>));
        }

        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(UserIDto dto)
        //{
        //    //Temp method, so 
        //    _tempDb.Users.Add(new User() { Login = dto.Name });
        //    await _tempDb.AsContext().SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> SelectionAction([FromForm] SelectionIDto sel)
        //{
        //    var uservice = (DBTokenService)_userService;
        //    switch (sel.Action)
        //    {
        //        case "Delete":
        //            await uservice.DeleteUsers(sel.Selection);
        //            break;
        //        default:
        //            throw new ArgumentException($"Unsupported button value: {sel.Action}");
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
