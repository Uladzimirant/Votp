using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Votp.Contracts.Services;
using Votp.DS.Entities;
using Votp.Exceptions;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Tokens.Time.Controllers;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Totp.Controllers;
using Votp.Tokens.Totp.Entities;

namespace Votp.Controllers.Admin
{
    public class TokensController : Controller
    {
        private ILogger<TokensController> _l;
        private readonly ITokenService _tokenService;
        private readonly IMapper _m;
        public TokensController(ILogger<TokensController> l, IMapper mapper, ITokenService tokenService, IUserService userService)
        {
            _l = l;
            _m = mapper;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            return View((await _tokenService.GetTokens()).Select(_m.Map<TokenODto>));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TokenIDto dto)
        {
            try
            {
                await _tokenService.CreateToken(_m.Map<Token>(dto));
                return RedirectToAction(nameof(Index));
            }
            catch (ExpectedException e)
            {
                return View("ErrorMessage", e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SelectionAction([FromForm] SelectionIDto sel)
        {
            switch (sel.Action)
            {
                case "Delete":
                    await _tokenService.DeleteTokens(sel.Selection);
                    break;
                case "Disable":
                    await _tokenService.DisableTokens(sel.Selection);
                    break;
                case "Enable":
                    await _tokenService.EnableTokens(sel.Selection);
                    break;
                default:
                    throw new ArgumentException($"Unsupported button value: {sel.Action}");
            }
            return RedirectToAction(nameof(Index));
        }

        private readonly static Dictionary<Type, Type> _controllerMap = new Dictionary<Type, Type>
            {
                { typeof(TimeToken), typeof(SystemTimeTokenController) },
                { typeof(TotpToken), typeof(TotpTokenController) }
            };

        public async Task<IActionResult> Delete(int id)
        {
            await _tokenService.DeleteTokens(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) 
        {
            var tokens = await _tokenService.GetTokens();
            var token = tokens.Single(t => t.Id == id);
            return Redirect($"{_controllerMap[token.GetType()].Name.Replace("Controller","")}?id={id}");
        }

        //// GET: AdminController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
