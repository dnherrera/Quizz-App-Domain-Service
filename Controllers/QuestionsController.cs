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
        private readonly IQuizAppRepository _quizAppRepository;

        public QuestionsController(IQuestionRepository questionRepository, IQuizAppRepository quizAppRepository)
        {
            _questionRepository = questionRepository;
            _quizAppRepository = quizAppRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionList()
        {
            IEnumerable<Question> questions = await _questionRepository.GetQuestionListAsync();
                return Ok(questions);
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            Question question = await _questionRepository.GetQuestionAsync(questionId);
                if (question == null)
                    return NotFound($"Question {questionId} not found");

                return Ok(question);
        }

        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuestionQuizId(int quizId)
        {
            Question question = await _questionRepository.GetQuestionByQuizIdAsync(quizId);
                if (question == null)
                    return NotFound($"Quiz - {quizId} not found");

                return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question) 
        {
           Quiz quiz = await _quizAppRepository.GetQuizAsync(question.QuizId);
           if(quiz == null)
                return NotFound($"QuizId {question.QuizId} not found");

           await _questionRepository.CreateQuestionsAsync(question);
           return Accepted(question);
        }

        [HttpPut("{QuestionId}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int QuestionId, [FromBody] Question question)
        {
            try
            {
                Question questionToUpdate = await _questionRepository.GetQuestionAsync(QuestionId);
                questionToUpdate.QuestionContent = question.QuestionContent == null ? questionToUpdate.QuestionContent : question.QuestionContent;
                questionToUpdate.CorrectAnswer = question.CorrectAnswer == null ? questionToUpdate.CorrectAnswer : question.CorrectAnswer;
                questionToUpdate.Answer1 = question.Answer1 == null ? questionToUpdate.Answer1 : question.Answer1;
                questionToUpdate.Answer2 = question.Answer2 == null ? questionToUpdate.Answer2 : question.Answer2;
                questionToUpdate.Answer3 = question.Answer3 == null ? questionToUpdate.Answer3 : question.Answer3;
                questionToUpdate.QuizId = question.QuizId;

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