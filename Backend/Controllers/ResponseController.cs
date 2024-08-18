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

            await _responseService.SaveResponseAsync(responseDto);
            return Ok();
        }
    }
}

