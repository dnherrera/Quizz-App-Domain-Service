using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.API.Data;
using QuizApp.API.Models;

namespace QuizApp.API.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly QuizAppDbContext _quizAppDbContext;

        public AuthRepository(QuizAppDbContext quizAppDbContext)
        {
            _quizAppDbContext = quizAppDbContext;
        }

        public async Task<UserModel> LoginAsync(string username, string password)
        {
            UserModel user = await _quizAppDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;
            
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // auth success
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<UserModel> RegisterAsync(UserModel user, string password)
        {
             byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _quizAppDbContext.AddAsync(user);
            await _quizAppDbContext.SaveChangesAsync();

            return user;
        }

         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
             if (await _quizAppDbContext.Users.AnyAsync(u => u.Username == username))
                return true;
            
            //if not exists
            return false;
        }
    }
}