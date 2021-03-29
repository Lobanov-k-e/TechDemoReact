using System;
using System.ComponentModel.DataAnnotations;
using TechDemo.Core.Domain;
using TechDemo.Core.Infrastructure.Mappings;

namespace TechDemo.Core.Infrastructure.Services
{
    public class UserVm : IMapFrom<User>
    {
        public int Id { get; set; }  

        [Required(ErrorMessage = "Введите имя")]  
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]        
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }
    }
}