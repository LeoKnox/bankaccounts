using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bankAccount.Models
{
    public class Users
    {
        [Key]
        public int UsersId {get; set;}
        [Required]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public String FirstName {get; set;}
        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        public String LastName {get; set;}
        [Required]
        [MinLength(8)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public String Email {get; set;}
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Must be at least 8 Characters")]
        public String Password {get; set;}

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

       [NotMapped]
       [Compare("Password")]
       [DataType(DataType.Password)]
       public string cPassword {get; set;}
    }
}