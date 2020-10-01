using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MyLogicApp_sample.BLL.Account;
using MyLogicApp_sample.Web.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyLogicApp_sample.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string ReturnUrl)
        {
            var userid = HttpContext.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userid))
            {
                var returnUrl = ReturnUrl;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    try
                    {
                        var url = returnUrl.Split('/');
                        if (url.Length == 4)
                            return RedirectToAction(url[2], url[1], new { id = url[3] });
                        return RedirectToAction(url[2], url[1]);
                    }
                    catch
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel() { ReturnUrl = ReturnUrl };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userManager = MembershipTools.NewUserManager();

            var user = await userManager.FindAsync(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı bulunmamaktadır!");
                return View(model);
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, userIdentity);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}