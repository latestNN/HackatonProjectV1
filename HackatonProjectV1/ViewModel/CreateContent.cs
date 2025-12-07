using HackatonProjectV1.Entities.MainPageElements;

namespace HackatonProjectV1.ViewModel
{
    public class CreateContent
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public ContentLabel Label { get; set; }

        public int UserId { get; set; }

        public int UnivercityId { get; set; }

        public int FacultyId { get; set; }

        public int DepeartmentId { get; set; }
    }
}
