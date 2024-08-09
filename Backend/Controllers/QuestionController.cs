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
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto question)
        {
            if (question == null) return BadRequest("Question data is null.");
            var addedQuestion = await _questionService.AddQuestionAsync(question);
            return Ok(addedQuestion);
        }
        
    }
}
