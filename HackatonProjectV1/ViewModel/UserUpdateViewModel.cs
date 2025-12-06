using HackatonProjectV1.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackatonProjectV1.ViewModel
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Üniversite")]
        [Required(ErrorMessage = "Üniversite alanı boş geçilemez")]
        public string University { get; set; }

        [Display(Name = "Fakülte")]
        [Required(ErrorMessage = "Fakülte alanı boş geçilemez")]
        public string Faculty { get; set; }
        
        [Display(Name = "Bölüm")]
        [Required(ErrorMessage = "Bölüm alanı boş geçilemez")]
        public string Department { get; set; }
        
        [Display(Name = "Sınıf")]
        [Required(ErrorMessage = "Sınıf alanı boş geçilemez")]
        public string Class { get; set; }

        public List<AppUser> UnaprrovedUsers { get; set; }

    }
}
