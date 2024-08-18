using Backend.Model.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestionnaire([FromBody] QuestionnaireDto questionnaireDto)
        {
            if (questionnaireDto == null)
                return BadRequest("Invalid data.");

            var questionnaireId = await _questionnaireService.CreateQuestionnaireAsync(questionnaireDto);
            return Ok(new { QuestionnaireID = questionnaireId });
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("getallquestionnaires")]
        public async Task<IActionResult> GetAllQuestionnaire()
        {
            var questionnaire = await _questionnaireService.GetAllQuestionnairesWithQuestionsAsync();

            if (questionnaire == null)
                return NotFound("Questionnaire not found.");

            return Ok(questionnaire);
        }

        [Authorize(Roles = "Vendor,Admin")]
        [HttpGet("{questionnaireId}")]
        public async Task<IActionResult> GetQuestions(int questionnaireId)
        {
            var questionnaire = await _questionnaireService.GetQuestionsByQuestionnaireIdAsync(questionnaireId);

            if (questionnaire == null)
                return NotFound("Questionnaire not found.");

            return Ok(questionnaire);
        }
    }
}
