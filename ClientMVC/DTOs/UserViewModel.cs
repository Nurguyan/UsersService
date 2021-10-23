using ClientMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Dtos
{
    public class UserViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int Id { get; set; }
        [Display(Name = "Фамилия")]
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Display(Name = "Возраст")]
        [Required]
        [Range(0, 150)]
        public int Age { get; set; }
        [Display(Name = "Пол")]
        [Required]
        public ESex Sex { get; set; }
        [Display(Name = "Активен")]
        [Required]
        public bool IsActive { get; set; }
        [Display(Name = "Номера телефонов")]
        public IEnumerable<PhoneViewModel> Phones { get; set; } //список номеров телефонов

        public UserViewModel()
        {
            Phones = new List<PhoneViewModel>();
        }
    }
}
