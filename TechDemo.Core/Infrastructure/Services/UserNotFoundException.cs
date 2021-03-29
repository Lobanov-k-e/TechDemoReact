using System;

namespace TechDemo.Core.Infrastructure.Services
{
    public class UserNotFoundException : Exception
    {
         public UserNotFoundException(object key)
            : base($"User with ({key}) was not found.")
        {
        }
    }
}
