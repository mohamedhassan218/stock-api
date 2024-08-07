using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public ApplicationDbContext(DbContextOptions dbContextOptions) :
            base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName= "ADMIN"
                },
                new IdentityRole
                {
                    Name= "User",
                    NormalizedName= "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}