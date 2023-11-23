//using AuthorizeLibrary.IdentityModel;
using DBModels.AppModels;
using DBModels.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public DbSet<Claimant> Claimants { get; set; }

        public override int SaveChanges()
        {
            var state = new EntityState[] { EntityState.Added, EntityState.Modified, EntityState.Deleted };
            var change = ChangeTracker.Entries();
            return base.SaveChanges();  
        }

    }
}