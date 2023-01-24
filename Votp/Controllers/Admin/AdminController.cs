using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Votp.Models.Request;
using Votp.Models.Response;
using Votp.Services.Contracts;

namespace Votp.Controllers.Admin
{
    public class AdminController : Controller
    {
        // GET: AdminController
        private ILogger<AdminController> _l;
        private ITokenService _tokenService;
        private IUserService _userService;
        public AdminController(ILogger<AdminController> l, ITokenService tokenService, IUserService userService)
        {
            _l = l;
            _tokenService = tokenService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Tokens));
        }

        public IActionResult Tokens()
        {
            return View(_tokenService.GetTokens());
        }

        public IActionResult Users()
        {
            return View(_userService.GetUsers());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TokenIDto dto)
        {
            _tokenService.CreateToken(dto);
            return RedirectToAction(nameof(Tokens));
        }

        [HttpPost]
        public IActionResult SelectionAction([FromForm] SelectedTokensIDto selection)
        {
            switch (selection.Action)
            {
                case "Delete":
                    _tokenService.DeleteTokens(selection.Tokens);
                    break;
                case "Disable":
                    _tokenService.DisableTokens(selection.Tokens);
                    break;
                case "Enable":
                    _tokenService.EnableTokens(selection.Tokens);
                    break;
                default:
                    throw new ArgumentException($"Unsupported button value: {selection.Action}");
            }
            return RedirectToAction(nameof(Tokens));
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
