using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IAuthRepository
    {
        Task<Users> RegisterAsync (Users user, string password);
        Task<Users> LoginAsync (string username, string password);
        Task<bool> UserExistsAsync (string username);
    }
}