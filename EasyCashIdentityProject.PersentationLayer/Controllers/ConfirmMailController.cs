using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int id)
        {
            var value = TempData["Mail"];
            ViewBag.Email = value;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailView)
        {
            var user = await _userManager.FindByEmailAsync(confirmMailView.Mail);

            if (user.ConfirmCode == confirmMailView.ConfirmCode)
            {
                user.EmailConfirmed= true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");
            }
            return View(confirmMailView);
        }
    }
}
