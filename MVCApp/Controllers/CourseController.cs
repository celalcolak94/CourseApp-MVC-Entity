using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Models;
using MVCAppIntro.Data;

namespace MVCApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index(int sayfa = 1, string aranan = "", string fiyatSiralama = "asc")
        {
            var db = new TestDbContext();

            //Course çekilirken CourseTeacher bilgisini de çek      yani joinle
            //Eğet CourseTeacherId null değilse kayıt gelir
            var courses = db.Courses.Include(x => x.CourseTeacher)
                .Where(x => x.StartDate.Value > DateTime.Now.Date)   //Henüz başlamamış kursları getir.
                .Where(x => EF.Functions.Like(x.Name, "%" + aranan + "%"))
                .OrderBy(x => x.StartDate).ToList(); //başlangıç saatine göre sırala



            if (fiyatSiralama == "asc")
            {
                courses = courses.OrderBy(x => x.Price).ToList();
            }
            else if (fiyatSiralama == "desc")
            {
                courses = courses.OrderByDescending(x => x.Price).ToList();
            }


            //Sıraladıktan sonra tekrar sıralamaya göre sayfalansın diye kodu buraya aldık.
            //Sıralamaya göre sayfalama doğru çalışsın diye yaptık

            courses = courses.Skip((sayfa - 1) * 5) //kayıt atlatma
                .Take(5) //kayıt alma
                .ToList();


            //Sayfa sayısını hesaplama algoritması

            //Select Count(*) form Courses db.Courses.Count()
            //Her sayfada 5 adet kayıt görüneceği için kayıt sayısını 5 böldük ve sayfa sayısını bulduk
            double kayitSayisi = Convert.ToDouble(db.Courses
                .Where(x => x.StartDate.Value > DateTime.Now.Date)
                .Where(x => EF.Functions.Like(x.Name, "%" + aranan + "%"))
                .Count());
            double sayfaSayisi = Convert.ToInt32(Math.Ceiling(kayitSayisi / 5)); //Yukarı yuvarla
            //11/5 = 2.1 => 3e yuvarlamamız lazım
            //Sayfa sayısıda tam sayı olacağı convert.toint ile sayfa sayısını int  tamsayı yaptık

            ViewBag.SayfaSayisi = sayfaSayisi;
            ViewBag.OncekiSayfa = sayfa == 1 ? 1 : sayfa - 1; // sayfa 1 ise zaten önceki sayfa olmaz
            ViewBag.SonrakiSayfa = sayfa == sayfaSayisi ? sayfaSayisi : sayfa + 1;
            //Son sayfadaysam zaten sayfa sondur, değilsem sonraki sayfa şuanki sayfa + 1 olur
            ViewBag.Aranan = aranan;
            ViewBag.Sayfa = sayfa;
            ViewBag.FiyatSiralama = fiyatSiralama;

            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            //Kodun veri tabanına gitmeden önce kontrolden geçmesi olayına validasyon denir.

            if (course.StartDate > course.EndDate)
            {
                ModelState.AddModelError("StartDate", "Başlangıç tarihi bitiş tarihinden büyük olamaz.");
            }


            //Validasyondan geçtiysek(kontrol)
            if (ModelState.IsValid)
            {
                var db = new TestDbContext();
                db.Courses.Add(course);
                db.SaveChanges();

                ModelState.Clear();

                ViewBag.message = "Kayıt Başarılı";
            }

            return View();
        }

        [HttpGet]
        public IActionResult AssingTeacherToCourse(int id)
        {
            var db = new TestDbContext();
            ViewBag.Teachers = db.Teachers.ToList();  //Öğretmenler Listesi
            ViewBag.Courses = db.Courses.ToList();  //Kurslar Listesi

            var c = db.Courses.Find(id);
            ViewBag.Course = c.Name;

            return View();
        }

        //Bazı durumlarda ekran databasedeki modeller için yetersiz kalır. Fazladan databasedeki modellerin alanlarını formdan göndermek yerine sadece formdan gönderilecek olan alanları belirleyip model klasörü içerisine tanımlarız.
        [HttpPost]
        public IActionResult AssingTeacherToCourse(CourseTeacher courseTeacher)
        {
            var db = new TestDbContext();
            ViewBag.Teachers = db.Teachers.ToList();  //Öğretmenler Listesi
            ViewBag.Courses = db.Courses.ToList();  //Kurslar Listesi

            if (ModelState.IsValid)
            {
                var c = db.Courses.FirstOrDefault(x => x.Name == courseTeacher.CourseName);
                c.CourseTeacherId = courseTeacher.TeacherId;
                db.Courses.Update(c);
                db.SaveChanges();

                ModelState.Clear();

                ViewBag.message = "Kayıt Başarılı";
            }

            return View();
        }
    }
}
