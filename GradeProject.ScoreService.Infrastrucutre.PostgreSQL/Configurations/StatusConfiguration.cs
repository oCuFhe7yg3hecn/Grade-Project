using GradeProject.ScoreService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Infrastrucutre.PostgreSQL.Configurations
{
    class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Users)
                   .WithOne(us => us.Status)
                   .HasForeignKey(us => us.StatusId);
        }
    }
}
