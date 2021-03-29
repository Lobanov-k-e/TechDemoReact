using Microsoft.AspNetCore.Http;
using System;

namespace TechDemo.Core.Infrastructure.Services
{
    public class CreateUserVm
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
    
