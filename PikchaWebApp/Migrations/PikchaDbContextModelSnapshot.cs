﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PikchaWebApp.Data;

namespace PikchaWebApp.Migrations
{
    [DbContext(typeof(PikchaDbContext))]
    partial class PikchaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasMaxLength(50000);

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SubjectId")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SubjectId")
                        .HasColumnType("varchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("PikchaRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("PikchaRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PikchaUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("PikchaUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PikchaUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("PikchaUserTokens");
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageProduct", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ImageId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsSale")
                        .HasColumnName("IsSale")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("DECIMAL(13,2)");

                    b.Property<string>("PrinterId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SellerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Type")
                        .HasColumnName("Type")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("PrinterId");

                    b.HasIndex("SellerId");

                    b.ToTable("ImageProducts");
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageTag", b =>
                {
                    b.Property<string>("ImageTagId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PikchaImageId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("ImageTagId", "PikchaImageId");

                    b.HasIndex("PikchaImageId");

                    b.ToTable("ImageTags");
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageView", b =>
                {
                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("PikchaImageId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .HasColumnType("longtext");

                    b.HasKey("Date", "PikchaImageId");

                    b.HasIndex("PikchaImageId");

                    b.ToTable("ImageViews");
                });

            modelBuilder.Entity("PikchaWebApp.Models.ArtistFollower", b =>
                {
                    b.Property<string>("ArtistsId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("ArtistsId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ArtistFollowers");
                });

            modelBuilder.Entity("PikchaWebApp.Models.PikchaImage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ArtistId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Caption")
                        .HasColumnName("Caption")
                        .HasColumnType("longtext");

                    b.Property<int>("Height")
                        .HasColumnName("Height")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnName("Location")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("ModifiedAt")
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Thumbnail")
                        .HasColumnName("Thumbnail")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnName("Title")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("UploadedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UploadedAt")
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Watermark")
                        .HasColumnName("Watermark")
                        .HasColumnType("longtext");

                    b.Property<int>("Width")
                        .HasColumnName("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("PikchaWebApp.Models.PikchaUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Addr1")
                        .HasColumnName("Addr1")
                        .HasColumnType("longtext");

                    b.Property<string>("Addr2")
                        .HasColumnName("Addr2")
                        .HasColumnType("longtext");

                    b.Property<string>("Avatar")
                        .HasColumnName("Avatar")
                        .HasColumnType("longtext");

                    b.Property<string>("Bio")
                        .HasColumnName("Bio")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .HasColumnName("City")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnName("Country")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FName")
                        .HasColumnName("Fname")
                        .HasColumnType("longtext");

                    b.Property<string>("InvSign")
                        .HasColumnName("InvSign")
                        .HasColumnType("longtext");

                    b.Property<string>("LName")
                        .HasColumnName("Lname")
                        .HasColumnType("longtext");

                    b.Property<string>("Links")
                        .HasColumnName("Links")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Postal")
                        .HasColumnName("Postal")
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Sign")
                        .HasColumnName("Sign")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .HasColumnName("State")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("PikchaUsers");
                });

            modelBuilder.Entity("PikchaWebApp.Models.Printer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("PikchaWebApp.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PikchaWebApp.Models.PikchaUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageProduct", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaImage", "Image")
                        .WithMany("Products")
                        .HasForeignKey("ImageId");

                    b.HasOne("PikchaWebApp.Models.Printer", "Printer")
                        .WithMany()
                        .HasForeignKey("PrinterId");

                    b.HasOne("PikchaWebApp.Models.PikchaUser", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageTag", b =>
                {
                    b.HasOne("PikchaWebApp.Models.Tag", "Tag")
                        .WithMany("Tags")
                        .HasForeignKey("ImageTagId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PikchaWebApp.Models.PikchaImage", "PikchaImage")
                        .WithMany("Tags")
                        .HasForeignKey("PikchaImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PikchaWebApp.Models.ImageView", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaImage", "PikchaImage")
                        .WithMany("Views")
                        .HasForeignKey("PikchaImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PikchaWebApp.Models.ArtistFollower", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaUser", "Artist")
                        .WithMany("Followers")
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PikchaWebApp.Models.PikchaUser", "PikchaUser")
                        .WithMany("Following")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("PikchaWebApp.Models.PikchaImage", b =>
                {
                    b.HasOne("PikchaWebApp.Models.PikchaUser", "Artist")
                        .WithMany("Images")
                        .HasForeignKey("ArtistId");
                });
#pragma warning restore 612, 618
        }
    }
}
