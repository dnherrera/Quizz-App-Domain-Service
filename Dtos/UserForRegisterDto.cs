using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Dtos
{
    /// <summary>
    /// User for Register DTO
    /// </summary>
    public class UserForRegisterDto
    {
        /// <summary>
        /// Gets or sets Username
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets Password
        /// </summary>
        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }
    }
}