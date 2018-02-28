using System.ComponentModel.DataAnnotations;

namespace belt2.Models
{
    public class RegisterUserModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name="User Name")]
        public string Name {get; set;}
        [Required]
        [MinLength(2)]
        [Display(Name="Alias")]
        public string Alias {get; set;}
        [Required]
        [EmailAddress]
        [Display(Name="Email Address")]
        public string EmailAddress {get; set;}
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password {get; set;}
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword {get; set;}
    }
}
