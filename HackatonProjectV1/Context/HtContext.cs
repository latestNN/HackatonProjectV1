 using HackatonProjectV1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackatonProjectV1.Context
{
    public class HtContext : IdentityDbContext<AppUser, IdentityRole , string>
    {
        public HtContext(DbContextOptions<HtContext> options) : base(options)
        { 
        }
    }
}
