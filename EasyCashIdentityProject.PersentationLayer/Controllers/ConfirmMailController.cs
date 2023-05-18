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
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (user.ConfirmCode == confirmMailView.ConfirmCode)
            {
                await _userManager.ConfirmEmailAsync(user, token);
                return RedirectToAction("Index", "MyProfile");
            }
            return View(confirmMailView);
        }
    }
}
