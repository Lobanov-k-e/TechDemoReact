using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TechDemo.Core.Infrastructure.Services
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file);

        void RemovePhoto(string photo);

    }
}
