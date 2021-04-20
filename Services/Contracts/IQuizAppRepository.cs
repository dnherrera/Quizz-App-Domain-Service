using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    /// <summary>
    /// Quiz Repo Interface
    /// </summary>
    public interface IQuizAppRepository
    {
        /// <summary>
        /// Get Quiz List Async
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QuizModel>> GetQuizListAsync();

        /// <summary>
        /// Create Quiz Async
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        Task<QuizModel> CreateQuizAsync (QuizModel quiz);

        /// <summary>
        /// Get Quiz Async
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        Task<QuizModel> GetQuizAsync(int quizId);
    }
}