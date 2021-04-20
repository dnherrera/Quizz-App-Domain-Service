using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.API.Models;
using QuizApp.API.Services;

namespace QuizApp.API.Controllers
{
    /// <summary>
    /// Quizzes Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizAppRepository _quizAppRepository;
        private readonly IQuestionRepository _questionRepository;

        /// <summary>
        /// Intializes a new instance of <seealso cref="QuizzesController"/>
        /// </summary>
        /// <param name="quizAppRepository"></param>
        /// <param name="questionRepository"></param>
        public QuizzesController(IQuizAppRepository quizAppRepository, IQuestionRepository questionRepository)
        {
            _quizAppRepository = quizAppRepository;
            _questionRepository = questionRepository;
        }

        /// <summary>
        /// Get Quizzes List By Owner Id
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetQuizzesListByOwnerId()
        {
            // Find the user id using Claims Principal Find First 
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            IEnumerable<QuizModel> quizzes = (await _quizAppRepository.GetQuizListAsync()).Where(q => q.OwnerId == userId);
            
            return Ok(quizzes);
        }

        /// <summary>
        /// Get All Quizzes
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            IEnumerable<QuizModel> quizzes = await _quizAppRepository.GetQuizListAsync();
            return Ok(quizzes);
        }

        /// <summary>
        /// Get Quiz by Id
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuizById(int quizId)
        {
            QuizModel quiz = await _quizAppRepository.GetQuizAsync(quizId);
                
            if (quiz == null)
                 return NotFound($"Quiz '{quizId}' not found");

            return Ok(quiz);
        }

        /// <summary>
        /// Create Quiz
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostQuiz([FromBody] QuizModel quiz) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Find Current User Id
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            quiz.OwnerId = currentUserId;
            var createdQuiz = await _quizAppRepository.CreateQuizAsync(quiz);

            return Accepted(createdQuiz);
        }

        /// <summary>
        /// Edit Quiz
        /// </summary>
        /// <param name="QuizId"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPut("{quizId}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int quizId, [FromBody] QuizModel quiz)
        {
            try
            {
                QuizModel quizToUpdate = await _quizAppRepository.GetQuizAsync(quizId);
                
                quizToUpdate.Title = quiz.Title;

                if (!await _questionRepository.SaveAll())
                    throw new Exception($"Updating quiz '{quizId}' failed on save.");

                return Accepted(quizToUpdate);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}