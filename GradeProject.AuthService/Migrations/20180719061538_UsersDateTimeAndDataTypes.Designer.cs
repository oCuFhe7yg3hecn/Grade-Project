﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GradeProject.AuthService.Migrations
{
    [DbContext(typeof(UsersContext))]
    [Migration("20180719061538_UsersDateTimeAndDataTypes")]
    partial class UsersDateTimeAndDataTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GradeProject.AuthService.Models.User", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("RegisteredAt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("SubjectId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
