using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechDemo.Core.Infrastructure.Services
{
    public interface IUserService
    {
        Task<int> CreateAsync(CreateUserVm model);
        Task DeleteAsync(int id);
        Task<IEnumerable<UserVm>> GetAllAsync();
        Task UpdateAsync(UpdateUserVm model);
        Task<UserVm> GetByIdAsync(int id); 
    }
}