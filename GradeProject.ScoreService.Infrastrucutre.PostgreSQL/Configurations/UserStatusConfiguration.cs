using GradeProject.ScoreService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Infrastrucutre.PostgreSQL.Configurations
{
    class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.HasKey(us => new { us.UserId, us.StatusId });
        }
    }
}
