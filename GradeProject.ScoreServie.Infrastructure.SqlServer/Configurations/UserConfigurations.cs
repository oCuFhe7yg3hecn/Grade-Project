using GradeProject.ScoreService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreServie.Infrastructure.SqlServer
{
    class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.HasMany(u => u.ScoreInfo)
                   .WithOne(s => s.User);

            builder.HasMany(u => u.Status)
                   .WithOne(us => us.User)
                   .HasForeignKey(us => us.UserId);
        }
    }
}
