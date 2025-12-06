using System.ComponentModel.DataAnnotations;

namespace HackatonProjectV1.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad-Soyad alanı zorunludur.")]
        [Display(Name = "Ad - Soyad")]
        public string NameAndLastname { get; set; }


        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC No 11 haneli olmalıdır.")]
        [Display(Name = "T.C. Kimlik No")]
        public string TcNo { get; set; }

        [Required(ErrorMessage = "Öğrenci Belgesi zorunludur.")]
        [Display(Name = "Öğrenci Belgesi")]
        public string StudentBarcode { get; set; }
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
