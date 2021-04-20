using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    /// <summary>
    /// Question Interface
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Get Question List Async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QuestionModel>> GetQuestionListAsync();

        /// <summary>
        /// Create Question Async
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<QuestionModel> CreateQuestionsAsync (QuestionModel question);

        /// <summary>
        /// Get Question By Id Async
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<QuestionModel> GetQuestionByIdAsync(int Id);

        /// <summary>
        /// Get Question By Quiz Id
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        Task<IEnumerable<QuestionModel>> GetQuestionByQuizIdAsync(int quizId);

        /// <summary>
        /// Save All
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAll();
    }
}