using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Data;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public class QuizRepository : IQuizAppRepository
    {
        private readonly QuizAppDbContext _quizAppDbContext;

        public QuizRepository(QuizAppDbContext quizAppDbContext)
        {
            _quizAppDbContext = quizAppDbContext;
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            await _quizAppDbContext.Quizzes.AddAsync(quiz);
            await _quizAppDbContext.SaveChangesAsync();
            return quiz;
        }

        public async Task<IEnumerable<Quiz>> GetQuizListAsync()
        {
             return await _quizAppDbContext.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuizAsync(int quizId)
        {
            return await _quizAppDbContext.Quizzes .Where(c => c.Id == quizId).FirstOrDefaultAsync();
        }
    }
}