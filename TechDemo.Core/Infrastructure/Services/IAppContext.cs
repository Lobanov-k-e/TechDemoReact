using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TechDemo.Core.Domain;

namespace TechDemo.Core.Infrastructure.Services
{
    public interface IAppContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}