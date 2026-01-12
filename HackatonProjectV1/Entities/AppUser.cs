using HackatonProjectV1.Entities.MainPageElements;
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

        public ICollection<Content> contents { get; set; }

        public ICollection<Comments> comments { get; set; }
        public ICollection<Complaint> complaints { get; set; }

        public int? UniversityId { get; set; }

        public University? university { get; set; }

        public int? facultyId { get; set; }

        public Faculty? faculty { get; set; }

        public int? departmentId { get; set; }

        public Department? department { get; set; }
    }
}
