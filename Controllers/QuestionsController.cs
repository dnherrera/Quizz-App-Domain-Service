using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApp.API.Models;
using QuizApp.API.Services;

namespace QuizApp.API.Controllers
{
    /// <summary>
    /// Question Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizAppRepository _quizAppRepository;

        /// <summary>
        /// Intializes a new instance of <seealso cref="QuestionsController"/>
        /// </summary>
        /// <param name="questionRepository"></param>
        /// <param name="quizAppRepository"></param>
        public QuestionsController(IQuestionRepository questionRepository, IQuizAppRepository quizAppRepository)
        {
            _questionRepository = questionRepository;
            _quizAppRepository = quizAppRepository;
        }

        /// <summary>
        /// Get Question List
        /// </summary>
        /// <returns>IEnumerable Question List. </returns>
        [HttpGet]
        public async Task<IActionResult> GetQuestionList()
        {
            IEnumerable<QuestionModel> questions = await _questionRepository.GetQuestionListAsync();
            return Ok(questions);
        }

        /// <summary>
        /// Get Question By Id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetQuestionById(int questionId)
        {
            QuestionModel question = await _questionRepository.GetQuestionByIdAsync(questionId);
                
            if (question == null)
                    return NotFound($"Question '{questionId}' not found");
                
            return Ok(question);
        }

        /// <summary>
        /// Get Question By Quiz Identifier
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuestionQuizId(int quizId)
        {
            IEnumerable<QuestionModel> question = await _questionRepository.GetQuestionByQuizIdAsync(quizId);
               
            if (question == null)
                 return NotFound($"Quiz '{quizId}' not found");
                
            return Ok(question);
        }

        /// <summary>
        /// Post a Question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] QuestionModel question) 
        {
           QuizModel quiz = await _quizAppRepository.GetQuizAsync(question.QuizId);
           if(quiz == null)
                return NotFound($"QuizId {question.QuizId} not found");

           await _questionRepository.CreateQuestionsAsync(question);
           
            return Accepted(question);
        }

        /// <summary>
        /// Edit a Question
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int questionId, [FromBody] QuestionModel request)
        {
            try
            {
                var questionToUpdate = await _questionRepository.GetQuestionByIdAsync(questionId);

                // Coalescing Operator - binary operator to check for null values.
                questionToUpdate.QuestionContent = request.QuestionContent ?? questionToUpdate.QuestionContent;
                questionToUpdate.CorrectAnswer = request.CorrectAnswer ?? questionToUpdate.CorrectAnswer;
                questionToUpdate.Answer1 = request.Answer1  ?? questionToUpdate.Answer1;
                questionToUpdate.Answer2 = request.Answer2 ?? questionToUpdate.Answer2;
                questionToUpdate.Answer3 = request.Answer3 ?? questionToUpdate.Answer3;
                questionToUpdate.QuizId = request.QuizId;

                if (!await _questionRepository.SaveAll())
                    throw new Exception($"Updating question '{questionId}' failed on save.");

                return Accepted(questionToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
    }
}