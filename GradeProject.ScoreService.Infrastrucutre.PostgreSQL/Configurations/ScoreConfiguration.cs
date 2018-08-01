using GradeProject.ScoreService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Infrastrucutre.PostgreSQL.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<ScoreInfo>
    {
        public void Configure(EntityTypeBuilder<ScoreInfo> builder)
        {
            builder.HasKey(s => s.Id);
            builder
               .Property(s => s.Id)
               .HasDefaultValueSql("uuid_generate_v4()");

            builder
                .Property(s => s.GameId)
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(s => s.GameName).IsRequired();
            builder.Property(s => s.LastUpdate).IsRequired();
            builder.Property(s => s.Score).IsRequired();
        }
    }
}
