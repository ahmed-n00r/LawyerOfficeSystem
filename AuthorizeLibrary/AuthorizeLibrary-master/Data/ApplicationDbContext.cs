//using AuthorizeLibrary.IdentityModel;
using DBModels;
using DBModels.AppModels;
using DBModels.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Emit;

namespace AuthorizeLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext()
        {

        }

        public static string DBConnctionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            builder.UseSqlServer(DBConnctionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

            
        }

        public DbSet<ContacteType> ContacteTypes { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<Lowyer> lowyers { get; set; }
        public DbSet<Claimant> Claimants { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var state = new EntityState[] { EntityState.Added, EntityState.Modified, EntityState.Deleted };
            var changeSet = ChangeTracker.Entries().Where(c => state.Contains(c.State));
            foreach(var item in changeSet)
            {
                if(!(item is null))
                {
                    var entityModel = item.Entity as MainModel;
                    if (item.State == EntityState.Added && !(entityModel is null))
                    {
                        entityModel.addModel();
                    }
                    else if(item.State == EntityState.Modified && !(entityModel is null))
                    {
                        entityModel.updateModel();
                    }
                    else if (item.State == EntityState.Deleted && !(entityModel is null))
                    {
                        entityModel.deleteModel();
                        item.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}