namespace HackatonProjectV1.Entities.MainPageElements
{
    public enum ContentLabel
    {
        Homework = 0,
        Event = 1,
        Question = 2,
        Project = 3,
        Note = 4,
        Source = 5,
        Announcemen = 6,
        Exam = 7
    }
    public class Content
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Like { get; set; }
        public int? Dislike { get; set; }

        public ContentLabel Label { get; set; }

        public string? UserId { get; set; }

        public AppUser? User { get; set; }

        public ICollection<Comments> comments { get; set; }

        public int UniversityId { get; set; }

        public University university { get; set; }

        public int? FacultyId { get; set; }

        public Faculty? faculty { get; set; }

        public int? departmentId { get; set; }

        public Department? department { get; set; }


    }
}
