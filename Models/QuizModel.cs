using System.ComponentModel.DataAnnotations;

namespace QuizApp.API.Models
{
    /// <summary>
    /// Quiz Model
    /// </summary>
    public class QuizModel
    {
        /// <summary>
        /// Gets or sets the Quiz Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Quiz Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public string OwnerId { get; set; }
    }
}