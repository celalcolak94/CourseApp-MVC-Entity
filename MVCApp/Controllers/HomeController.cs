using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            var plist = new List<ProductModel>();

            var p1 = new ProductModel();
            p1.İsim = "Ürün-1";
            p1.Kategori = "Giyim";
            p1.Fiyat = 10;
            p1.Stok = 10;

            plist.Add(p1);

            var p2 = new ProductModel
            {
                İsim = "Ürün-2",
                Kategori = "Giyim",
                Fiyat = 100,
                Stok = 20
            };

            plist.Add(p2);

            // View'e action içinde oluşturduğumuz veriyi gönderiyoruz.

            return View(plist);
        }
    }
}
