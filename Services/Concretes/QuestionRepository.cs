using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Data;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    /// <summary>
    /// Question Repository
    /// </summary>
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizAppDbContext _quizAppDbContext;

        /// <summary>
        /// Initializes a new instance of <seealso cref="QuestionRepository"/>
        /// </summary>
        /// <param name="quizAppDbContext"></param>
        public QuestionRepository(QuizAppDbContext quizAppDbContext)
        {
            _quizAppDbContext = quizAppDbContext;
        }

        /// <summary>
        /// Create Question Async
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<QuestionModel> CreateQuestionsAsync(QuestionModel question)
        {
            await _quizAppDbContext.Questions.AddAsync(question);
            await _quizAppDbContext.SaveChangesAsync();
            return question;
        }

        /// <summary>
        /// Get Question By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<QuestionModel> GetQuestionByIdAsync(int Id)
        {
            return await _quizAppDbContext.Questions .Where(c => c.Id == Id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Question By Quiz Id
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QuestionModel>> GetQuestionByQuizIdAsync(int quizId)
        {
            return await _quizAppDbContext.Questions .Where(c => c.QuizId == quizId).ToListAsync();
        }

        /// <summary>
        /// Get Question List Async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<QuestionModel>> GetQuestionListAsync()
        {
             return await _quizAppDbContext.Questions.ToListAsync();
        }

        /// <summary>
        /// Save Changes if greater than zero
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAll()
        {
             return await _quizAppDbContext.SaveChangesAsync() > 0;
        }
    }
}