namespace HackatonProjectV1.Entities.MainPageElements
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UniversityId { get; set; }

        public University University { get; set; }

        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; }

        public ICollection<AppUser> appUsers { get; set; }

        public ICollection<Content> contents { get; set; }
    }
}
