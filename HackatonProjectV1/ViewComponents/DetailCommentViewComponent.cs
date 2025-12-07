using HackatonProjectV1.Context;
using HackatonProjectV1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.ViewComponents
{
    public class DetailCommentViewComponent : ViewComponent
    {
        private readonly HtContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DetailCommentViewComponent(HtContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(int id)
        {
            var values = _context.comments.Include(x => x.User).Where(x => x.contentId == id).ToList();
            return View(values);
        }
    }
}
