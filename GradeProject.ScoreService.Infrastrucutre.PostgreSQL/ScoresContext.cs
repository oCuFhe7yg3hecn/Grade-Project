using GradeProject.ScoreService.Domain;
using GradeProject.ScoreService.Infrastrucutre.PostgreSQL.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Infrastrucutre.PostgreSQL
{
    public class ScoresContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ScoreInfo> Scores { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }

        public ScoresContext(DbContextOptions opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.ApplyConfiguration<User>(new UserConfigurations());
            modelBuilder.ApplyConfiguration<Status>(new StatusConfiguration());
            modelBuilder.ApplyConfiguration<ScoreInfo>(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration<UserStatus>(new UserStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}


//Server=localhost;Port=54321;User Id=artur;Password=knightbetter45;Database=JournPractise;
