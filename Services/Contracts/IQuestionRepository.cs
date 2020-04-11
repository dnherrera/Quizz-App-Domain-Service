using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetQuestionListAsync();
        Task<Question> CreateQuestionsAsync (Question question);
        Task<Question> GetQuestionAsync(int Id);
        Task<IEnumerable<Question>> GetQuestionByQuizIdAsync(int quizId);
        Task<bool> SaveAll();
    }
}