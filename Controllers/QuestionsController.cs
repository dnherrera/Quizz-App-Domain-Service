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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionList()
        {
            IEnumerable<Question> questions = await _questionRepository.GetQuestionListAsync();
                return Ok(questions);
        }

        [HttpGet("{QuestionId}")]
        public async Task<IActionResult> GetQuestion(int QuestionId)
        {
            Question question = await _questionRepository.GetQuestionAsync(QuestionId);
                if (question == null)
                    return NotFound($"Question {QuestionId} not found");

                return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question) 
        {
           await _questionRepository.CreateQuestionsAsync(question);
           return Accepted(question);
        }

        [HttpPut("{QuestionId}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int QuestionId, [FromBody] Question question)
        {
            try
            {
                Question questionToUpdate = await _questionRepository.GetQuestionAsync(QuestionId);
                questionToUpdate.QuestionContent = question.QuestionContent;
                if (!await _questionRepository.SaveAll())
                    throw new Exception($"Updating qeustion {QuestionId} failed on save");

                return Accepted(questionToUpdate);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
    }
}