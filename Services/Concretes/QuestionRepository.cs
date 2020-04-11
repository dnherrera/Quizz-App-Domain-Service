using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Data;
using QuizApp.API.Models;
using QuizApp.API.Services;

namespace QuizApp.API.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuizAppDbContext _quizAppDbContext;

        public QuestionRepository(QuizAppDbContext quizAppDbContext)
        {
            _quizAppDbContext = quizAppDbContext;
        }
        public async Task<Question> CreateQuestionsAsync(Question question)
        {
            await _quizAppDbContext.Questions.AddAsync(question);
            await _quizAppDbContext.SaveChangesAsync();
            return question;
        }

        public async Task<Question> GetQuestionAsync(int Id)
        {
            return await _quizAppDbContext.Questions .Where(c => c.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionByQuizIdAsync(int quizId)
        {
            return await _quizAppDbContext.Questions .Where(c => c.QuizId == quizId).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionListAsync()
        {
             return await _quizAppDbContext.Questions.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
             return await _quizAppDbContext.SaveChangesAsync() > 0;
        }
    }
}