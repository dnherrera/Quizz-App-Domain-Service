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
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizAppRepository _quizAppRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuizzesController(IQuizAppRepository quizAppRepository, IQuestionRepository questionRepository)
        {
            _quizAppRepository = quizAppRepository;
            _questionRepository = questionRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetQuizzesList()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Quiz> quizzes = (await _quizAppRepository.GetQuizListAsync()).Where(q => q.OwnerId == userId);
            return Ok(quizzes);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuizzes()
        {
            IEnumerable<Quiz> quizzes = await _quizAppRepository.GetQuizListAsync();
            return Ok(quizzes);
        }

        [HttpGet("{QuizId}")]
        public async Task<IActionResult> GetQuiz(int QuizId)
        {
            Quiz quiz = await _quizAppRepository.GetQuizAsync(QuizId);
                if (quiz == null)
                    return NotFound($"Quiz {QuizId} not found");

                return Ok(quiz);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostQuiz([FromBody] Quiz quiz) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            quiz.OwnerId = currentUserId;
           await _quizAppRepository.CreateQuizAsync(quiz);

            return Accepted(quiz);
        }

        [HttpPut("{QuizId}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int QuizId, [FromBody] Quiz quiz)
        {
            try
            {
                Quiz quizToUpdate = await _quizAppRepository.GetQuizAsync(QuizId);
                quizToUpdate.Title = quiz.Title;
                if (!await _questionRepository.SaveAll())
                    throw new Exception($"Updating quiz {QuizId} failed on save");

                return Accepted(quizToUpdate);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}