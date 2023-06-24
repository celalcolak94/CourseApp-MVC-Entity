using Microsoft.AspNetCore.Mvc;
using MVCApp.Data;
using MVCAppIntro.Data;

namespace MVCApp.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            var db = new TestDbContext();
            var tlist = db.Teachers.ToList();

            return View(tlist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            var db = new TestDbContext();
            db.Teachers.Add(teacher);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var db = new TestDbContext();
            var teacher = db.Teachers.Find(id);

            return View(teacher);
        }

        [HttpPost]
        public IActionResult Update(Teacher teacher) //student formdan gelen yeni student
        {
            var db = new TestDbContext();
            db.Teachers.Update(teacher);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var db = new TestDbContext();
            var teacher = db.Teachers.Find(id);

            return View(teacher);
        }

        [HttpPost]
        public IActionResult Delete(Teacher teacher)
        {
            //Silinecek olan kayıtlarda id alanın hiddenInput ile   yakalamak yeterlidir.
            var db = new TestDbContext();
            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
