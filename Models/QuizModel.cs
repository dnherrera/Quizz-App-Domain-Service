using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    public class QuizModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

         public string OwnerId { get; set; }
    }
}