using BookstoreWeb.Models;
using BookstoreWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookstoreWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            (userManager, signInManager) = (userMgr, signInMgr);

            // БД Identity будет заполняться каждый раз, когда создается
            // объект AccountController для обработки НТТР-запроса
            IdentityInitializer.Initialize(userMgr).Wait();
        }

        public ViewResult Login(string returnUrl) => View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin");
                }
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(loginModel);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
