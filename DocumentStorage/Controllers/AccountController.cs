// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the AccountController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Security;

    using DocumentStorage.DAL.Data;
    using DocumentStorage.DAL.Repository;
    using DocumentStorage.Helpers;
    using DocumentStorage.Models.Account;

    /// <summary>
    /// The account controller.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ValidationHelper.ValidateUser(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password) }))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed.");
                }
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserRepository.Create(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password) }))
                {
                    if (ValidationHelper.ValidateUser(new User { UserName = model.UserName, Password = ValidationHelper.Encode(model.Password) }))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return this.RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Login failed.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The provided username already exists.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
            }

            return this.View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }
    }
}
