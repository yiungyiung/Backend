using Backend.Model;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public IActionResult CreateQuestionnaireAssignment([FromBody] QuestionnaireAssignmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.CreateAssignments(dto);
            return Ok(new { message = "Assignments created successfully." });
        }

        [HttpGet("{id}")]
        public IActionResult GetAssignmentById(int id)
        {
            var assignment = _service.GetAssignmentById(id);

            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(assignment);
        }

        [HttpGet("vendor/{vendorId}")]
        public IActionResult GetAssignmentsByVendorId(int vendorId)
        {
            var assignments = _service.GetAssignmentsByVendorId(vendorId);
            return Ok(assignments);
        }
    }
}
