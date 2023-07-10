using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Data.Config;

namespace MVCAppIntro.Data
{
    public class TestDbContext : DbContext
    {
        //Student sınıfının veritabanı tarafında bir tablo olması için DbSet propertysi kullanıyoruz.
        //Students ismi tabloya bağlanmak için kullanacağımız isim

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique(); //Unique olsun
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique(); //Unique olsun
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(12); //En fazla 12 karakter
            modelBuilder.Entity<User>().HasKey(x => x.Id); //PK alanı belirttik
            //modelBuilder.Entity<User>().ToTable("Kullanıcılar"); //Dbde tablo adını değiştirdik.
            //modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("KullanıcıAdı"); //Sütun ismini değiştirdik.

            //Kodu burada yazmak yerine klasörden getirme yöntemi var. Büyük projelerde burasının okunması zorlaştığı için bu durumda buradaki kodları classlara geçirebiliriz.

            //Config dosyasına yazdık
            //modelBuilder.Entity<Role>().HasMany<User>();

            //config dosyasından oku.
            modelBuilder.ApplyConfiguration(new RoleConfig());

            base.OnModelCreating(modelBuilder);
        }

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
