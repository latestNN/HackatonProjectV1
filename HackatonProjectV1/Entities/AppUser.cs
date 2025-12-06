using Microsoft.AspNetCore.Identity;

namespace HackatonProjectV1.Entities
{
    public class AppUser : IdentityUser
    {
        public string NameAndLastname { get; set; }
        

        public string TcNo { get; set; }

        public bool IsApproved { get; set; }
        public string StudentBarcode { get; set; }

        public string? University { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Class { get; set; }
    }
}
