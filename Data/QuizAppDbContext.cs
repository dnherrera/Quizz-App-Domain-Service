using Microsoft.EntityFrameworkCore;
using QuizApp.API.Models;

namespace QuizApp.API.Data
{
    /// <summary>
    /// Quiz App DB Context
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext"/>
    public class QuizAppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <seealso cref="QuizAppDbContext"/>
        /// </summary>
        /// <param name="options"></param>
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base (options) {}

        /// <summary>
        /// Question Model
        /// </summary>
        public DbSet<QuestionModel> Questions { get; set; }

        /// <summary>
        /// Quiz Model
        /// </summary>
        public DbSet<QuizModel> Quizzes { get; set; }

        /// <summary>
        /// User Model
        /// </summary>
        public DbSet<UsersModel> Users {get;set;}
    }
}