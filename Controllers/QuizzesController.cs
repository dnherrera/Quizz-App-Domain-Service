using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> GetQuizzesList()
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

        [HttpPost]
        public async Task<IActionResult> PostQuiz([FromBody] Quiz quiz) 
        {
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