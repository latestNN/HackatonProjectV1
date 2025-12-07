namespace HackatonProjectV1.Entities.MainPageElements
{
    public class Comments
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int? contentId { get; set; }

        public Content? content { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        public DateTime? CreateTime { get; set; }


    }
}
