using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IAuthRepository
    {
        Task<UserModel> RegisterAsync (UserModel user, string password);
        Task<UserModel> LoginAsync (string username, string password);
        Task<bool> UserExistsAsync (string username);
    }
}