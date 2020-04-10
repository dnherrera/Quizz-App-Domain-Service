using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify a password between 4 and 8 characters.")]
        public string password { get; set; }
    }
}