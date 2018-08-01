using GradeProject.ScoreService.Domain;
using GradeProject.ScoreService.Infrastrucutre.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeProject.ScoreServie.Infrastructure.PostgreSQL.Configurations
{
    public static class Seeder
    {
        public static void Seed(ScoresContext context)
        {
            var users = new List<User>()
                {
                    new User()
                    {
                        Total = 250
                    },
                    new User()
                    {
                        Total = 120
                    },
                    new User()
                    {
                        Total = 340
                    },
                    new User()
                    {
                        Total = 1250
                    },
                    new User
                    {
                        Total = 321
                    }
                };

            var scores = new List<ScoreInfo>()
            {
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[0],
                    GameName = "Memorizer",
                    Score = 221
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[0],
                    GameName = "Pacman",
                    Score = 14

                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[0],
                    GameName = "Tetris",
                    Score = 325
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[0],
                    GameName = "Book Rush",
                    Score = 221
                },


                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[1],
                    GameName = "Memorizer",
                    Score = 111
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[1],
                    GameName = "BookRush",
                    Score = 125
                },

                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[2],
                    GameName = "Memorizer",
                    Score = 111
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[2],
                    GameName = "Arcanoid",
                    Score = 111
                },


                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[4],
                    GameName = "Arcanoid",
                    Score = 11
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[4],
                    GameName = "Memorizer",
                    Score = 111
                },
                new ScoreInfo()
                {
                    Id = Guid.NewGuid(),
                    User = users[3],
                    GameName = "Road Way Play",
                    Score = 111
                }
            };

            var statuses = new List<Status>()
            {
                new Status()
                {
                    Name = "Banned"
                },
                new Status()
                {
                    Name = "Promoted"
                },
                new Status()
                {
                    Name = "Under Check"
                }
            };

            var userStatuses = new List<UserStatus>()
            {
                new UserStatus()
                {
                    Status = statuses[1],
                    User = users[0]
                },
                new UserStatus()
                {
                    Status = statuses[1],
                    User = users[1]
                },
                new UserStatus()
                {
                    Status = statuses[1],
                    User = users[3]
                },
                new UserStatus()
                {
                    Status = statuses[0],
                    User = users[2]
                },
                new UserStatus()
                {
                    Status = statuses[2],
                    User = users[4]
                },
            };

            if (!context.Users.Any()) { context.Users.AddRange(users); }
            if (!context.Scores.Any()) { context.AddRange(scores); }
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(statuses);
                context.UserStatus.AddRange(userStatuses);
            }
            if (!context.UserStatus.Any()) { context.AddRange(userStatuses);  }
            context.SaveChanges();
        }
    }
}
