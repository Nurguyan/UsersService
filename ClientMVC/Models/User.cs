using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public ESex Sex { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Phone> Phones { get; set; } //список номеров телефонов

        public User()
        {
            Phones = new List<Phone>();
        }
    }

    // из ISO/IEC 5218 https://en.wikipedia.org/wiki/ISO/IEC_5218
    public enum ESex
    {
        [Display(Name = "Неизвестно")]
        NotKnown = 0,
        [Display(Name = "Мужчина")]
        Male = 1,
        [Display(Name = "Женщина")]
        Female = 2,
        [Display(Name = "Неприменимо")]
        NotApplicable = 9
    }
}
