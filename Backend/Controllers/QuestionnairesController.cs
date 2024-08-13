using Backend.Model.DTOs;
using Backend.Services;
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

        [HttpPost]
        public async Task<IActionResult> CreateQuestionnaire([FromBody] QuestionnaireDto questionnaireDto)
        {
            if (questionnaireDto == null)
                return BadRequest("Invalid data.");

            var questionnaireId = await _questionnaireService.CreateQuestionnaireAsync(questionnaireDto);
            return Ok(new { QuestionnaireID = questionnaireId });
        }
    }
}
