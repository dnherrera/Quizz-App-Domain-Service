using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    public class Users
    {
         [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}