using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username boş geçilemez.")]
        public string UserName { get; set; }

        [MinLength(8,ErrorMessage = "Parola min 8 karakter uzunluğunda olmalıdır.")]
        [Required(ErrorMessage = "Parola boş geçilemez.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } // Beni hatırla
    }
}
