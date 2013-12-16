using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider _provider;

        public AccountController(IAuthProvider provider)
        {
            // TODO: Complete member initialization
            this._provider = provider;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_provider.Authenticate(model.UserName, model.Password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                    ModelState.AddModelError("", "Incorrect username or password");
            }

            return View();
        }
    }
}
