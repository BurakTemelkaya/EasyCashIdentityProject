using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IConfiguration _configuration;

        public RegisterController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                var code = random.Next(100000, 1000000);
                AppUser appuser = new()
                {
                    UserName = appUserRegisterDto.UserName,
                    Name = appUserRegisterDto.Name,
                    SurName = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    City = "İstanbul",
                    District = "Sancaktepe",
                    ImageUrl = "imageUrl",
                    ConfirmCode = code.ToString(),
                };
                var result = await _userManager.CreateAsync(appuser, appUserRegisterDto.Password);
                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new();
                    MailboxAddress mailboxAddressFrom = new("Easy Cash Admin", _configuration.GetSection("Mail:Gmail:Email").Value);
                    MailboxAddress mailboxAddressTo = new("User", appuser.Email);

                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    BodyBuilder bodyBuilder = new();

                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz:" + code;

                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Easy Cash Kayıt Olmak İçin Onay Kodu";

                    SmtpClient client = new();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_configuration.GetSection("Mail:Gmail:Email").Value, _configuration.GetSection("Mail:Gmail:Password").Value);

                    client.Send(mimeMessage);

                    client.Disconnect(true);

                    TempData["Mail"] = appUserRegisterDto.Email;

                    return RedirectToAction("Index", "ConfirmMail");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(appUserRegisterDto);
        }
    }
}
