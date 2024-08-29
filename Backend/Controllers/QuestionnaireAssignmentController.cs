using Backend.Model;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager,Vendor")]
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
        [Authorize(Roles = "Admin,Manager,Vendor")]
        [HttpGet("vendor/{vendorId}")]
        public IActionResult GetAssignmentsByVendorId(int vendorId)
        {
            var assignments = _service.GetAssignmentsByVendorId(vendorId);
            return Ok(assignments);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult GetAllAssignments()
        {
            var assignments = _service.GetAllAssignments();
            return Ok(assignments);
        }
    }
}
