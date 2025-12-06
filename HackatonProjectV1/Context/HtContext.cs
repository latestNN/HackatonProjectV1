using HackatonProjectV1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Context
{
    public class HtContext : IdentityDbContext<AppUser>
    {
        public HtContext(DbContextOptions<HtContext> options) : base(options)
        { 
        }
    }
}
