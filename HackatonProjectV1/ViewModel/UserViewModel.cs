using System.ComponentModel.DataAnnotations;
using HackatonProjectV1.Entities;
using HackatonProjectV1.Entities.MainPageElements;

namespace HackatonProjectV1.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı gereklidir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad Soyad gereklidir")]
        [Display(Name = "Ad Soyad")]
        public string NameAndLastname { get; set; }

        [Display(Name = "TC No")]
        public string TcNo { get; set; }

        [Display(Name = "Öğrenci Numarası")]
        public string StudentBarcode { get; set; }

        [Display(Name = "Sınıf")]
        public string Class { get; set; }

        [Display(Name = "Onay Durumu")]
        public bool IsApproved { get; set; }

        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Üniversite")]
        public int? UniversityId { get; set; }

        [Display(Name = "Fakülte")]
        public int? FacultyId { get; set; }

        [Display(Name = "Bölüm")]
        public int? DepartmentId { get; set; }

        public University? university { get; set; }
        public Faculty? faculty { get; set; }
         public Department? department { get; set; }
    }
}
