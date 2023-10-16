using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDtos
    {
        [Required]
        public string Username {get;set;}
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password {get;set;}
        public string FullName {get;set;}
        public int Gender {get;set;}
        public DateOnly DateofBirth {get;set;}
        public int PhoneNumber {get;set;}
        public string Address {get;set;}
        public string Email {get;set;}
    }
}