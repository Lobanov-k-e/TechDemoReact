using System;
using System.Linq;
using System.Threading.Tasks;
using TechDemo.Core.Domain;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemo.Core
{
    public static class SeedData
    {
        public static async Task Seed(IAppContext context)
        {
            if (context.Users.Count() == 0)
            {
                var users = Enumerable.Range(1, 5).Select(i =>
                    new User
                    {
                        Name = $"Name{i}",
                        Password = $"password{i}",
                        Email = $"example{i}@example.com",
                        DateOfBirth = DateTime.Parse("2000-08-18T07:22:16.0000000-07:00").AddDays(i),
                        Photo = $"photo_{i}.jpg"
                    }).ToList();

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }
                
        }
    }
}
