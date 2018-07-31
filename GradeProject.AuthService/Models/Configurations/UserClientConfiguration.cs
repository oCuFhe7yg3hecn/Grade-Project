using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Models.Configurations
{
    public class UserClientConfiguration : IEntityTypeConfiguration<UserClient>
    {
        public void Configure(EntityTypeBuilder<UserClient> builder)
        {
            builder.HasOne<User>(uc => uc.User).WithMany(u => u.Clients).HasForeignKey(u => u.UserId);
        }
    }
}
