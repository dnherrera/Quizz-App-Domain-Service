using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IQuizAppRepository
    {
        Task<IEnumerable<QuizModel>> GetQuizListAsync();
        Task<QuizModel> CreateQuizAsync (QuizModel quiz);
        Task<QuizModel> GetQuizAsync(int quizId);
    }
}