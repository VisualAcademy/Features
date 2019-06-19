using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Feature.Shared;
using System.Configuration;

namespace Feature.BlazorServer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
            // Empty
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // 공식과 같은 코드
        }
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Features;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<FeatureModel> Features { get; set; }
    }
}
