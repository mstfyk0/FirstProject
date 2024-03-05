using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //context class for the sql database    
    public class FitnessPlannerContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<User> Users { get; set; }
        public FitnessPlannerContext(DbContextOptions<FitnessPlannerContext> options ) : base( options ) { }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach ( var entry in entries )
            { 
                ((AuditableEntity)entry.Entity).LastModified = DateTime.UtcNow; 

                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity)entry.Entity).Created = DateTime.UtcNow;

                }
            
            }

            return await base.SaveChangesAsync();

        }


    }
}
