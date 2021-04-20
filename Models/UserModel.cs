using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Get or sets the Password Hash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the Password Salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}