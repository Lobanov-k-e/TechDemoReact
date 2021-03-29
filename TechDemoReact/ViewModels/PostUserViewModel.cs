using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechDemoReact.ViewModels
{
    public class PostUserViewModel
    {
        
        public string Name { get; set; }        
        public string Password { get; set; }        
        public string Email { get; set; }       
        public DateTime DateOfBirth { get; set; }       
        public string Photo { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
