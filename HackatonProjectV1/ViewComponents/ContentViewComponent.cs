using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using HackatonProjectV1.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.ViewComponents
{
    public class ContentViewComponent : ViewComponent
    {
        private readonly HtContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContentViewComponent(HtContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            // Label bir Enum olduğu için Include etmeye gerek yoktur.
            var contents = _context.contents
                                   .Include(x => x.User)
                                   .ToList();

            var faculties = _context.faculties.ToList();
            // 3. Bölümleri çek (View tarafında filtrelemek için FacultyId'ye ihtiyacımız olacak)
            var departments = _context.departments.ToList();

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
