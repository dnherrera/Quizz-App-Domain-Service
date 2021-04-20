using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    /// <summary>
    /// Question Model
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// Gets or set the Question Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Question Content
        /// </summary>
        public string QuestionContent { get; set; }

        /// <summary>
        /// Gets or sets the Correct Answer
        /// </summary>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// Gets or sets the Answer 1
        /// </summary>
        public string Answer1 { get; set; }

        /// <summary>
        /// Gets or sets the Answer 2
        /// </summary>
        public string Answer2 { get; set; }

        /// <summary>
        /// Gets or sets the Answer 3
        /// </summary>
        public string Answer3 { get; set; }

        /// <summary>
        /// Gets or sets the Quiz Id
        /// </summary>
        public int QuizId { get; set; }
    }
}