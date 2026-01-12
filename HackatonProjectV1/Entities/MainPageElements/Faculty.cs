namespace HackatonProjectV1.Entities.MainPageElements
{
    public class Faculty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UniversityId { get; set; }

        public University University { get; set; }

        public ICollection<Department> departments { get; set; }

        public ICollection<AppUser> appUsers { get; set; }

        public ICollection<Content> contents { get; set; }

        public ICollection<Complaint> complaints { get; set; }


    }
}
