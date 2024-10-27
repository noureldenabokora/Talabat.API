using System.ComponentModel.DataAnnotations;

namespace Talabat.ApIs.Dtos
{
    public class RegisterDto
    {
        [Required]  
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone] 
        public string PhoneNumber { get; set; }
        [Required]
       // [RegularExpression("(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[@$!%*#?~(&)+=^_-]).{8,}\r\n", ErrorMessage ="Password must have 1 Uppercase,1 Lowercase, 1 non alphanumric and at least 6 characters")]
        public string Password { get; set; }


    }
}
