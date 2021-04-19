using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionModel>> GetQuestionListAsync();
        Task<QuestionModel> CreateQuestionsAsync (QuestionModel question);
        Task<QuestionModel> GetQuestionAsync(int Id);
        Task<IEnumerable<QuestionModel>> GetQuestionByQuizIdAsync(int quizId);
        Task<bool> SaveAll();
    }
}