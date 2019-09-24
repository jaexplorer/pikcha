using PikchaWebApp.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PikchaWebApp.Data
{
    public class PikchaDbContext : ApiAuthorizationDbContext<PikchaUser>
    {
        public PikchaDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<PikchaUser> PikchaUsers { get; set; }
        public DbSet<PikchaImage> PikchaImages { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PikchaUser>().ToTable("PikchaUsers");
            builder.Entity<IdentityRole>().ToTable("PikchaRoles");
            builder.Entity<IdentityUserRole<string>>().ToTable("PikchaUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("PikchaUserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("PikchaUserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("PikchaRoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("PikchaUserTokens");

            builder.Entity<PikchaImage>()
            .Property(b => b.UploadedAt)
            .HasDefaultValueSql("getdate()");

            builder.Entity<PikchaImage>()
            .Property(b => b.ModifiedAt)
            .HasDefaultValueSql("getdate()");
        }

    }
}
