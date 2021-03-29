using System;
using System.Collections.Generic;
using System.Text;

namespace TechDemo.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
    }
}
