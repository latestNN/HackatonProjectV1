namespace HackatonProjectV1.Entities.MainPageElements
{
    public enum ComplaintLabel
    {
        Administrative = 0,
        Campus = 1,
        Transportation = 2,
        Exam = 3,
        Homework = 4,
        Schedule = 5,
        Grading = 6,
        Other = 7
    }
    public class Complaint
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? Support { get; set; }
        public int? Dislike { get; set; }

        public bool Status { get; set; } = true;

        public ComplaintLabel Label { get; set; }

        public string? UserId { get; set; }

        public AppUser? User { get; set; }

        public ICollection<Comments> comments { get; set; }

        public int? UniversityId { get; set; }

        public University? university { get; set; }

        public int? FacultyId { get; set; }

        public Faculty? faculty { get; set; }

        public int? departmentId { get; set; }

        public Department? department { get; set; }
    }
}
