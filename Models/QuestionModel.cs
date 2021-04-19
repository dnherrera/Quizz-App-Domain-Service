using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public string QuestionContent { get; set; }
        public string CorrectAnswer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int QuizId { get; set; }
    }
}