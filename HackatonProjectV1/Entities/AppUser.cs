using Microsoft.AspNetCore.Identity;

namespace HackatonProjectV1.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string TcNo { get; set; }

        public string StudentDocumentPath { get; set; }
    }
}
