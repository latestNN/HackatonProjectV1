using HackatonProjectV1.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackatonProjectV1.ViewModel
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }

        public bool IsApproved { get; set; }
        [Display(Name = "Üniversite")]
        [Required(ErrorMessage = "Lütfen üniversite seçiniz")]
        public int UniversityId { get; set; } // Artık string değil int tutuyoruz

        [Display(Name = "Fakülte")]
        [Required(ErrorMessage = "Lütfen fakülte seçiniz")]
        public int facultyId { get; set; } // Artık string değil int tutuyoru

        [Display(Name = "Bölüm")]
        [Required(ErrorMessage = "Lütfen bölüm seçiniz")]
        public int departmentId { get; set; } // Artık string değil int tutuyoru

        
        
        [Display(Name = "Sınıf")]
        [Required(ErrorMessage = "Sınıf alanı boş geçilemez")]

        
        public string Class { get; set; }

        public List<AppUser> UnaprrovedUsers { get; set; }

    }
}
