using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReactClient.Dtos
{
    public class PhoneViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните номер телефона")]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Некорректный номер телефона")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Некорректный номер телефона")]
        public string Number { get; set; }
    }
}
