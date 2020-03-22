using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IQuizAppRepository
    {
        Task<IEnumerable<Quiz>> GetQuizListAsync();
        Task<Quiz> CreateQuizAsync (Quiz quiz);
        Task<Quiz> GetQuizAsync(int quizId);
    }
}