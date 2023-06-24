using System.ComponentModel.DataAnnotations;

namespace MVCApp.Data
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name alanı boş geçilemez")]
        [MaxLength(50)] //50 karakter uzunluğunu geçemesin
        //Her zaman hangi alan içinse o alanın üstüne yazılması     gerekiyor.
        public string Name { get; set; }


        [Required(ErrorMessage = "Kurs içeriği girmek zorundasınız")]
        [MaxLength(200)]
        public string Content { get; set; } //İçerik


        //Required attribute çalışması için date,int,decimal gibi alanlar ? yani nullable tanımlı olmalıdır.
        //Eğer date,int,decimal gibi alanları nullable yapıp required tanımlarsak tekrar database migration ve update yapmamız gerekiyor. Çünkü required girdikten sonra not null olacak.
        [Range(45000,65000, ErrorMessage = "Kurs fiyatı 45 Bin ile 65 Bin arasında olabilir.")]
        [Required(ErrorMessage = "Fiyat alanı boş geçilemez")]
        public decimal? Price { get; set; } //Fiyat


        [Range(200,320, ErrorMessage = "Kurslar 200 ile 320 saat arasında planlanabilir.")]
        [Required(ErrorMessage = "Toplam Saat alanı boş geçilemez")]
        public int? TotalHours { get; set; } //Toplam Saat


        [Required(ErrorMessage = "Başlangıç Tarihi alanı boş geçilemez")]
        public DateTime? StartDate { get; set; } //Başlangıç Tarihi


        [Required(ErrorMessage = "Bitiş Tarihi alanı boş geçilemez")]
        public DateTime? EndDate { get; set; } //Bitiş tarihi

        //Join işlemleri için kullanırız.
        // Navigation property yani gezinti propertysi denir

        public int? CourseTeacherId { get; set; } //int? diyerek boş geçilebilir bir foreign key tanımı yapıyoruz. Daha sonradan öğretmen atanabilir diye.
        public Teacher? CourseTeacher { get; set; } //Kursun eğitmeni
        public List<Student>? CourseStudents { get; set; } //Kursa katılan öğrenciler

    }
}
