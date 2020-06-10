using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LifePlan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
            public DbSet<LifePlan.Models.DataModels.Company> Company { get; set; }
            public DbSet<LifePlan.Models.DataModels.Person> Person { get; set; }
            public DbSet<LifePlan.Models.DataModels.PersonCategory> PersonCategory { get; set; }

    }
}
