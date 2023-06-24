using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCAppIntro.Data;

namespace MVCApp.Controllers
{
    public class StudentController : Controller
    {
        //private TestDbContext db;

        //public StudentController(TestDbContext db)
        //{
        //    this.db = db;
        //}

        public IActionResult Index()
        {
            var db = new TestDbContext(); // sınıfa bağlan
            var slist = db.Students.ToList();  // selec * from students

            return View(slist);
        }

        //Sayfaya gelen ilk istekler ise Http Get ile yapılır. Action üzerine [HttpGet] yazmazsak mvc bu acion HttpGet olarak algılar. Bu sebeple formdan veri gönderirken HttpPost yazmak zorundayız.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Eğer bir sayfada formdan birşey gönderiliyorsa HttpPost olan action çağırılır.

        [HttpPost]
        public IActionResult Create(Student student) //student yeni öğrenci nesnesi
        {
            var db = new TestDbContext();
            db.Students.Add(student);
            db.SaveChanges();

            return RedirectToAction("Index");
            //Kayıttan sonra tablo listeleme sayfasına yönlendirir.
        }

        [HttpGet]
        public IActionResult Update(int id) // id => asp-route-id      den geliyor.
        {
            // id sine göre veri tabanındaki student tablosundan ilgili kaydı bulduk ki view dolduralım. Bu sayede form sayfası dolu görüntülenecek.
            var db = new TestDbContext();
            var student = db.Students.Find(id); //find methodu id      üzerinden bulur

            return View(student);
        }

        [HttpPost]
        public IActionResult Update(Student student) //student formdan gelen yeni student
        {
            var db = new TestDbContext();
            db.Students.Update(student);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var db = new TestDbContext();
            var student = db.Students.Find(id);
            //Delete sayfasında silinmek istenen kaydın ekranda     silmeden önce gösterilmesi için yaptık

            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            //Silinecek olan kayıtlarda id alanın hiddenInput ile   yakalamak yeterlidir.
            var db = new TestDbContext();
            db.Students.Remove(student);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
