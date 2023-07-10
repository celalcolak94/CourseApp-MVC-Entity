using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(); // uygulama Mvc þaablonu ile çalýþsýn

builder.Services.AddAuthentication(/*CookieAuthenticationDefaults.AuthenticationScheme*/"YZL3439")
    .AddCookie("YZL3439", option => //Uygulama cookie üzerinden imlik doðrulama yapsýn.
{
    option.LoginPath = "/Account/Login"; //Uygulama login sayfasýndan sürecine devam etsin.
});

// Razor Pages Proje Template
// MVC Web Application Proje Template
// API Web Servis geliþtireme Proje Template
// Blazor Web Application Template

var app = builder.Build();

app.UseStaticFiles();  //wwwroot altýndaki css,js dosyalarýna        güvenmesi için

app.UseRouting(); // uygulama da yönlendirme iþlemleri çalýþsýn.
// uygulama ilk açýldýðýnda anasayfadan açýlmasý için aþaðýdaki kuralý yazdýk.

app.UseAuthentication(); //Sistemde login süreci varsa authentication middleware     yazýyoruz.
app.UseAuthorization(); //Authorize attiribute çalýþsýn diye, yetkilendirme middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // uygulamadan gelen istekler controllera yönlendirilsin ayarý yaptýk.
app.Run();
