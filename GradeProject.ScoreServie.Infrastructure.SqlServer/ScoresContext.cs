using GradeProject.ScoreService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreServie.Infrastructure.SqlServer
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
            modelBuilder.ApplyConfiguration<User>(new UserConfigurations());
            modelBuilder.ApplyConfiguration<Status>(new StatusConfiguration());
            modelBuilder.ApplyConfiguration<ScoreInfo>(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration<UserStatus>(new UserStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
