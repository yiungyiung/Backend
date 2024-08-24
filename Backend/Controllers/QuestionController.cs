using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;
using Backend.Services;
using System.Threading.Tasks;
using Backend.Model.DTOs;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("add")]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto question)
        {
            if (question == null) return BadRequest("Question data is null.");
            var addedQuestion = await _questionService.AddQuestionAsync(question);
            return Ok(addedQuestion);
        }
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("framework/{frameworkId}")]
        public async Task<IActionResult> GetQuestionIdsByFramework(int frameworkId)
        {
            var questionIds = await _questionService.GetQuestionIdsByFrameworkAsync(frameworkId);
            if (questionIds == null || !questionIds.Any())
            {
                return NotFound($"No questions found for framework with ID {frameworkId}.");
            }
            return Ok(questionIds);
        }

    }
}
