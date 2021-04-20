using System.Threading.Tasks;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    /// <summary>
    /// Auth Repo Interface
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserModel> RegisterAsync (UserModel user, string password);

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserModel> LoginAsync (string username, string password);

        /// <summary>
        /// Is User Exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> IsUserExistsAsync (string username);
    }
}