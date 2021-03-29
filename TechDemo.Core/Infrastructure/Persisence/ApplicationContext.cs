using Microsoft.EntityFrameworkCore;
using TechDemo.Core.Domain;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemo.Core.Infrastructure.Persisence
{
    public class ApplicationContext : DbContext, IAppContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
