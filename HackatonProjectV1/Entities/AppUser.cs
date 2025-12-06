using Microsoft.AspNetCore.Identity;

namespace HackatonProjectV1.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string TcNo { get; set; }

        public bool IsApproved { get; set; }
        public string StudentBarcode { get; set; }
    }
}
