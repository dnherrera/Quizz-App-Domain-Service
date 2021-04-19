using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public interface IAuthRepository
    {
        Task<UsersModel> RegisterAsync (UsersModel user, string password);
        Task<UsersModel> LoginAsync (string username, string password);
        Task<bool> UserExistsAsync (string username);
    }
}