using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MVCApp.Data;
using MVCApp.Models;
using MVCAppIntro.Data;
using System.Security.Claims;

namespace MVCApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var db = new TestDbContext();

            //User rolleri joinle sonra user rolleri ile birlikte dbden bul
            var user = db.Users.Include(x => x.Roles).FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);


            if (user != null)
            {
                List<Claim> claims = new List<Claim>(); //Login olurken sistemde cookiede saklanacak kullanıcı değerlerini listeye atacağımız liste

                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }


                var identity = new ClaimsIdentity(claims, "YZL3439"); //Login olacak kişi için yukarıdaki özelliklerde bir kimlik oluşturduk.
                var claimPrinciple = new ClaimsPrincipal(identity); //Bu kimlik bilgilerini sisteme oturum açacak olan sınıfa atamasını yaptık.



                var authProps = new AuthenticationProperties(); //Yani kimlik doğrulama oluşacak olan cookie kalıcı olup olmayacağı gibi bilgileri bu sınıf üzerinden belirleriz.
                authProps.IsPersistent = model.RememberMe; //Cookie kalıcı olsun.        Session bazlı olmasın demek.

                if (model.RememberMe)
                {
                    authProps.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30);
                }
                //Eğer kullanıcı beni hatırlayı seçtiyse 1 aylık cookie

                HttpContext.SignInAsync(claimPrinciple, authProps); //Sistemde cookie oluşturmamızı sağlayacak kod.

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View(model);
            }

        }

        [Authorize(AuthenticationSchemes = "YZL3439")] //Oturum açmış bir hesap      sadece bu methodu çağırabilir. O yüzden authorize attribute koyduk.
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync("YZL3439"); //Authentication Scheme önemli,     yazmayı unutmamalıyız.

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User();
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.Password = model.Password;

                    var db = new TestDbContext();
                    db.Users.Add(user);
                    db.SaveChanges();

                    ViewBag.Message = "Kayıt başarılı yönlendiriliyorsunuz.";
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Email", "Aynı E-posta adresinden mevcut");
                    ModelState.AddModelError("UserName", "Aynı Username mevcut");

                }


                return View();

            }
            else
            {
                return View(model);
            }

        }
    }
}
