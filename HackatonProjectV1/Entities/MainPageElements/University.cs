namespace HackatonProjectV1.Entities.MainPageElements
{
    public class University
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? City { get; set; }

        public string? LogoImagePath { get; set; }
        public string? BackgroundImagePath { get; set; }

        public float? StarCount { get; set; }




        public ICollection<Faculty> faculties { get; set; }

        public ICollection<Department> departments { get; set; }

        public ICollection<AppUser> appUsers { get; set; }

        public ICollection<Content> contents { get; set; }
        public ICollection<Complaint> complaints { get; set; }
    }
}
