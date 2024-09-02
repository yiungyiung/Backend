using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveResponse([FromBody] ResponseDto responseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _responseService.SaveResponseAsync(responseDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the response.");
            }
        }
        [HttpPost("bulk")]
        public async Task<IActionResult> SaveAllResponses([FromBody] List<ResponseDto> responseDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _responseService.SaveAllResponsesAsync(responseDtos);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the responses.");
            }
        }
        // New endpoint to get all responses for a specific assignment
        [HttpGet("assignment/{assignmentId}")]
        public async Task<IActionResult> GetResponsesForAssignment(int assignmentId)
        {
            try
            {
                var responses = await _responseService.GetResponsesForAssignmentIdAsync(assignmentId);
                if (responses == null)
                {
                    return NotFound("No responses found for the given assignment ID.");
                }

                return Ok(responses);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the responses.");
            }
        }

        // New endpoint to get all responses for a specific questionnaire
        [HttpGet("questionnaire/{questionnaireId}")]
        public async Task<IActionResult> GetAllResponsesForQuestionnaire(int questionnaireId)
        {
            try
            {
                var responses = await _responseService.GetAllResponsesForQuestionnaireIdAsync(questionnaireId);
                if (responses == null || responses.Count == 0)
                {
                    return NotFound("No responses found for the given questionnaire ID.");
                }

                return Ok(responses);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the responses.");
            }
        }

        // New endpoint to get a specific response for a given assignment and question
        [HttpGet("assignment/{assignmentId}/question/{questionId}")]
        public async Task<IActionResult> GetResponseForAssignmentAndQuestion(int assignmentId, int questionId)
        {
            try
            {
                var response = await _responseService.GetResponseForAssignmentAndQuestionAsync(assignmentId, questionId);
                if (response == null)
                {
                    return NotFound("No response found for the given assignment ID and question ID.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the response.");
            }
        }
    }
}