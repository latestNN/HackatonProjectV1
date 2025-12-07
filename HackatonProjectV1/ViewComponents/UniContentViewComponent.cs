using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.ViewComponents
{
    public class UniContentViewComponent : ViewComponent
    {
        private readonly HtContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UniContentViewComponent(HtContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(HackatonProjectV1.Entities.MainPageElements.University university)
        {
            // Label bir Enum olduğu için Include etmeye gerek yoktur.
            int id = university.Id;
            var contents = _context.contents.Include(x => x.User)
                                   .Where(x => x.UniversityId == id)
                                   .ToList();

            ViewBag.UniversityName = university.Name;

            var faculties = _context.faculties.Where(x => x.UniversityId == id).ToList();
            // 3. Bölümleri çek (View tarafında filtrelemek için FacultyId'ye ihtiyacımız olacak)
            var departments = _context.departments.Where(x => x.UniversityId == id).ToList();

            var model = new MainpageDropBoxViewModel
            {
                Contents = contents,
                Faculties = faculties,
                Departments = departments
            };

            // 5. Modeli View'e gönder
            return View(model);
        }
    }
}
