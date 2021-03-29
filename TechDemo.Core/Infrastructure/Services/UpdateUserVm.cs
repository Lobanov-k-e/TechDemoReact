using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechDemo.Core.Infrastructure.Services
{
    public class UpdateUserVm
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile PhotoFile { get; set; }

        public static UpdateUserVm FormUser(UserVm user)
        {
            return new UpdateUserVm
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
    }
    }
}
