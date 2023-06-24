using Microsoft.EntityFrameworkCore;
using MVCApp.Data;

namespace MVCAppIntro.Data
{
    public class TestDbContext : DbContext
    {
        //Student sınıfının veritabanı tarafında bir tablo olması için DbSet propertysi kullanıyoruz.
        //Students ismi tabloya bağlanmak için kullanacağımız isim

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // bu method içerisinde database bağlantısı sağlayacağız
            // Server=localhost\SQLEXPRESS;Database=TestDb;uid=sa;pwd=1234;MultipleActiveResultSets=true;
            // yukarıda Sql Authentication ile user üzerinden bağlantı var
            // Windows Authentication "Server=localhost\SQLEXPRESS;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Test1Db;Trusted_Connection=True;MultipleActiveResultSets=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
