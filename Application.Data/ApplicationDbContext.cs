using Application.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("ApplicationConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }

        public DbSet<ApplicationRole> ApplicationRoles { set; get; }

        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }

        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public DbSet<Client> Clients { set; get; }

        public DbSet<RefreshToken> RefreshTokens { set; get; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(x => new { x.UserId, x.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(x => x.UserId).ToTable("ApplicationUserLogins");

            builder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
    }
}