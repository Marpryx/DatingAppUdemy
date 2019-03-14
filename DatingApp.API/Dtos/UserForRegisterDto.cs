using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        //In order to validate properties Username/Password we use [Required] flag
        [Required]
        public string Username { get; set; }

        [Required] //a built-in set of validation attributes that you can apply declaratively to any class or property.
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
    }
}