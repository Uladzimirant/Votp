using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services.Contracts;

namespace Votp.Controllers.Admin
{
    public class TokensController : Controller
    {
        private ILogger<TokensController> _l;
        private ITokenService _tokenService;
        public TokensController(ILogger<TokensController> l, ITokenService tokenService, IUserService userService)
        {
            _l = l;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _tokenService.GetTokens());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TokenIDto dto)
        {
            await _tokenService.CreateToken(dto);
            return RedirectToAction(nameof(Index));
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
