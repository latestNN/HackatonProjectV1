using HackatonProjectV1.Entities.MainPageElements;

namespace HackatonProjectV1.ViewModel
{
    public class MainpageDropBoxViewModel
    {
        public List<Content> Contents { get; set; }
        // Filtreler için veritabanından gelecek listeler
        public List<Faculty> Faculties { get; set; }
        public List<Department> Departments { get; set; }
    }
}
