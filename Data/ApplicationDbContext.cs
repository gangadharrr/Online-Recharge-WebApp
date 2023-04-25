using GoogleApi.Entities.Places.Search.Find.Response;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Online_Recharge_WebApp.Models;

namespace Online_Recharge_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RechargeProductModel> RechargeProduct { get; set; }
        public DbSet<CustomerSupport> CustomerSupport { get; set; }
       /* protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Candidate>().HasIndex(u => u.Email).IsUnique();
        }*/
    }
}