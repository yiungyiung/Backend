using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Backend.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;
        private readonly IConfiguration _configuration;
        public ResponseController(IResponseService responseService, IConfiguration configuration)
        {
            _responseService = responseService;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        [Authorize(Roles = "Vendor")]
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
        [Authorize(Roles = "Vendor")]
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
        [Authorize(Roles = "Admin,Manager,Analyst,Vendor")]
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
        [Authorize(Roles = "Admin,Manager,Analyst,Vendor")]
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
        [Authorize(Roles = "Admin,Manager,Analyst,Vendor")]
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Define file path and name
            var fileName = Path.GetFileName(file.FileName);
            var uploadPath = Path.Combine(_configuration["FileSettings:UploadPath"], fileName);

            // Ensure the upload directory exists
            var directory = Path.GetDirectoryName(uploadPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the file to the server
            try
            {
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            // Return successful response
            return Ok(new { FilePath = uploadPath, FileName = fileName });
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string filePath)
        {
            // Sanitize the file name to prevent directory traversal attacks
            var fileName = Path.GetFileName(filePath);

            // Define the file path
            var uploadPath = Path.Combine(_configuration["FileSettings:UploadPath"], fileName);

            // Ensure the file path is within the defined upload directory (to avoid directory traversal)
            if (!uploadPath.StartsWith(_configuration["FileSettings:UploadPath"]))
            {
                return BadRequest("Invalid file path.");
            }

            // Check if the file exists
            if (!System.IO.File.Exists(uploadPath))
            {
                return NotFound("File not found.");
            }

            // Try to determine the file's content type
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream"; // Fallback content type
            }

            try
            {
                // Read the file as a stream asynchronously
                var fileBytes = await System.IO.File.ReadAllBytesAsync(uploadPath);

                // Return the file to the client
                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                // _logger.LogError(ex, "Error while downloading file: {FileName}", fileName); // Optional logging
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while downloading the file.");
            }
        }



    }
}