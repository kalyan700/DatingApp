using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserToRegister
    {
        [Required]
        public string Username {get; set;}
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify the Password length between 4 and 8 charecters.")]
        public string Password {get; set;}
    }
}