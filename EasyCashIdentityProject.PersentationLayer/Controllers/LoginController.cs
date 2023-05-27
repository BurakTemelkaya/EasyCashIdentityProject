using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.IsPersistent, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "MyProfile");
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen email adresinizi doğrulayınız.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }

            return View(loginViewModel);
        }
    }
}
