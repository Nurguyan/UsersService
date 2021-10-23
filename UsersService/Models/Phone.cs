using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public User User { get; set; }
    }
}
