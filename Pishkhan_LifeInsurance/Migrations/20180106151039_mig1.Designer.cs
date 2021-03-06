﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pishkhan_LifeInsurance.Data;
using System;

namespace Pishkhan_LifeInsurance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180106151039_mig1")]
    partial class mig1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblKarmozd", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgentID");

                    b.Property<int>("Month");

                    b.Property<int>("Price");

                    b.Property<int>("TblLifeInsuranceID");

                    b.Property<int>("Year");

                    b.Property<bool>("isDouble");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("TblLifeInsuranceID");

                    b.ToTable("TblKarmozd");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblLifeInsurance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bimegozar_Name")
                        .IsRequired();

                    b.Property<string>("Bimegozar_Phone")
                        .HasMaxLength(11);

                    b.Property<string>("Bimeshode_Name")
                        .IsRequired();

                    b.Property<string>("Bimeshode_Phone")
                        .HasMaxLength(11);

                    b.Property<DateTime>("Date_Soodoor_Miladi");

                    b.Property<string>("Date_Soodoor_Shamsi")
                        .IsRequired();

                    b.Property<DateTime>("Date_Start_Miladi");

                    b.Property<string>("Date_Start_Shamsi")
                        .IsRequired();

                    b.Property<int>("InsuranceNumber_CenterCode");

                    b.Property<int>("InsuranceNumber_Code");

                    b.Property<string>("InsuranceNumber_GarardadNumber")
                        .IsRequired();

                    b.Property<int>("InsuranceNumber_Number");

                    b.Property<int>("InsuranceNumber_Year");

                    b.Property<bool>("Insurance_Status");

                    b.Property<int>("PaymentType");

                    b.Property<int>("Payment_Price");

                    b.Property<int?>("PishkhanID");

                    b.Property<int?>("SepordeAvaliye");

                    b.Property<int>("Serial");

                    b.Property<int?>("SupervisorID");

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("PishkhanID");

                    b.HasIndex("SupervisorID");

                    b.HasIndex("UserID");

                    b.ToTable("TblLifeInsurance");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblPishkhan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Pishkhan_Code");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("TblPishkhan");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblSetting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountTakeItem");

                    b.Property<int>("InsuranceNumber_CenterCode");

                    b.Property<int>("InsuranceNumber_Code");

                    b.Property<string>("InsuranceNumber_GarardadNumber")
                        .IsRequired();

                    b.Property<int>("InsuranceNumber_Year");

                    b.Property<int>("Pishkhan_Percent");

                    b.Property<int>("Pishkhan_Percent_OldInsurance");

                    b.Property<int>("Supervisior_Percent");

                    b.Property<int>("Supervisior_Percent_OldInsurance");

                    b.Property<int>("Supervisior_Percent_OutOfOffice");

                    b.HasKey("ID");

                    b.ToTable("TblSetting");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblSupervisior", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("TblSupervisior");
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Models.TblUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AgentID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblKarmozd", b =>
                {
                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser", "TblUser")
                        .WithMany("TblKarmozd")
                        .HasForeignKey("AgentID");

                    b.HasOne("Pishkhan_LifeInsurance.Data.DataBase.TblLifeInsurance", "TblLifeInsurance")
                        .WithMany("TblKarmozd")
                        .HasForeignKey("TblLifeInsuranceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pishkhan_LifeInsurance.Data.DataBase.TblLifeInsurance", b =>
                {
                    b.HasOne("Pishkhan_LifeInsurance.Data.DataBase.TblPishkhan", "TblPishkhan")
                        .WithMany("TblLifeInsurance")
                        .HasForeignKey("PishkhanID");

                    b.HasOne("Pishkhan_LifeInsurance.Data.DataBase.TblSupervisior", "TblSupervisior")
                        .WithMany("TblLifeInsurance")
                        .HasForeignKey("SupervisorID");

                    b.HasOne("Pishkhan_LifeInsurance.Models.TblUser", "TblUser")
                        .WithMany("TblLifeInsurance")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
