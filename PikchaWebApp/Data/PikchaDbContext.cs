﻿using PikchaWebApp.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

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
        //public DbSet<IdentityUserRole<string>> PikchaUserRoles { get; set; }

        
        public DbSet<PikchaImage> PikchaImages { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }
        public DbSet<ImageView> ImageViews { get; set; }

        public DbSet<ArtistFollower> Followers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }



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
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)") // mysql
            //.HasDefaultValueSql("getdate()") //mssql
            //.HasDefaultValueSql("date('now')") //sqlite
            ;

            builder.Entity<PikchaImage>()
            .Property(b => b.ModifiedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            //.HasDefaultValueSql("date('now')") //sqlite
            ;

            builder.Entity<PikchaUser>()
                .Property(b => b.Links)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));


            //ImageViews
            builder.Entity<ImageView>().HasKey(sc => new { sc.Date, sc.PikchaImageId }); 

            // pikcha tags
            builder.Entity<ImageTag>().HasKey(sc => new { sc.ImageTagId, sc.PikchaImageId });
            builder.Entity<ImageTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.Tags)
                .HasForeignKey(sc => sc.ImageTagId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ImageTag>()
                .HasOne<PikchaImage>(sc => sc.PikchaImage)
                .WithMany(s => s.Tags)
                .HasForeignKey(sc => sc.PikchaImageId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pikcha followers
            builder.Entity<ArtistFollower>().HasKey(sc => new { sc.ArtistsId, sc.UserId });
            builder.Entity<ArtistFollower>()
                .HasOne<PikchaUser>(sc => sc.PikchaUser)
                .WithMany(s => s.Following)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<ArtistFollower>()
                .HasOne<PikchaUser>(sc => sc.Artist)
                .WithMany(s => s.Followers)
                .HasForeignKey(sc => sc.ArtistsId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
