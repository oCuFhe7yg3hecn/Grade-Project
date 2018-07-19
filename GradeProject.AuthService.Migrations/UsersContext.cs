using GradeProject.AuthService.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Migrations
{
    public class UsersContext : DbContext
    {
        public enum SqlServerValueGenerationStrategy
        {
            IdentityColumn,
            SequenceHiLo
        }
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

}
}
