﻿// <auto-generated />
using GradeProject.AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GradeProject.AuthService.Migrations
{
    [DbContext(typeof(UsersContext))]
    partial class UsersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GradeProject.AuthService.Models.User", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeveloper");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("RegisteredAt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("SubjectId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GradeProject.AuthService.Models.UserClient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClient");
                });

            modelBuilder.Entity("GradeProject.AuthService.Models.UserClient", b =>
                {
                    b.HasOne("GradeProject.AuthService.Models.User", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
