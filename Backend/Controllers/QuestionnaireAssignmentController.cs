using Backend.Model;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireAssignmentController : ControllerBase
    {
        private readonly IQuestionnaireAssignmentService _service;

        public QuestionnaireAssignmentController(IQuestionnaireAssignmentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestionnaireAssignment([FromBody] QuestionnaireAssignmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CreateAssignments(dto);
            return Ok(new { message = "Assignments created successfully." });
        }

        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var assignment = await _service.GetAssignmentById(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }

        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("vendor/{vendorId}")]
        public async Task<IActionResult> GetAssignmentsByVendorId(int vendorId)
        {
            var assignments = await _service.GetAssignmentsByVendorId(vendorId);
            return Ok(assignments);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssignments()
        {
            var assignments = await _service.GetAllAssignments();
            return Ok(assignments);
        }

        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("questionnaire/{questionnaireId}")]
        public async Task<IActionResult> GetAssignmentsByQuestionnaireId(int questionnaireId)
        {
            var assignments = await _service.GetAssignmentsByQuestionnaireId(questionnaireId);
            return Ok(assignments);
        }
    }
}
