using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Data;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    /// <summary>
    /// Quiz Repository
    /// </summary>
    public class QuizRepository : IQuizAppRepository
    {
        private readonly QuizAppDbContext _quizAppDbContext;

        /// <summary>
        /// Initializes a new instance of <seealso cref="QuizRepository"/>
        /// </summary>
        /// <param name="quizAppDbContext"></param>
        public QuizRepository(QuizAppDbContext quizAppDbContext)
        {
            _quizAppDbContext = quizAppDbContext;
        }

        /// <summary>
        /// Create Quiz Async
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        public async Task<QuizModel> CreateQuizAsync(QuizModel quiz)
        {
            await _quizAppDbContext.Quizzes.AddAsync(quiz);
            await _quizAppDbContext.SaveChangesAsync();
            return quiz;
        }

        /// <summary>
        /// Get Quiz List Async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<QuizModel>> GetQuizListAsync()
        {
             return await _quizAppDbContext.Quizzes.ToListAsync();
        }

        /// <summary>
        /// Get Quiz by Id Async
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public async Task<QuizModel> GetQuizAsync(int quizId)
        {
            return await _quizAppDbContext.Quizzes .Where(c => c.Id == quizId).FirstOrDefaultAsync();
        }
    }
}