using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
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

        public  IViewComponentResult Invoke()
        {
            var values =  _context.contents.Include(x => x.User).ToList();
            return View(values);
        }
    }
}
