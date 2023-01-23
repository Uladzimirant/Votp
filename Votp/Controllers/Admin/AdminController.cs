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
        public AdminController(ILogger<AdminController> l, ITokenService tokenService) {
            _l = l;
            _tokenService = tokenService;
        }

        public ActionResult Index()
        {
            return RedirectToAction(nameof(Tokens));
        }

        public ActionResult Tokens()
        {
            return View(_tokenService.GetTokens());
        }

        public ActionResult Users()
        {
            return View(_tokenService.GetUsers());
        }

        [HttpPost]
        public IActionResult SelectionAction([FromForm] SelectedTokensIDto selection)
        {
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
