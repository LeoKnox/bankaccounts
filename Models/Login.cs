using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccount.Models
{
    public class Login
    {
        [Required]
        [MinLength(8)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public String Email {get; set;}
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Must be at least 8 Characters")]
        public String Password {get; set;}
    }
}