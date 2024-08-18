using Azure;
using Backend.Model.DTOs;
using Backend.Model;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class ResponseService : IResponseService
    {
        private readonly ApplicationDbContext _context;

        public ResponseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveResponseAsync(ResponseDto responseDto)
        {
            var responses = new Responses
            {
                AssignmentID = responseDto.AssignmentID,
                QuestionID = responseDto.QuestionID
            };

            _context.Responses.Add(responses);
            await _context.SaveChangesAsync();

            var optionResponse = new OptionResponses
            {
                ResponseID = responses.ResponseID,
                OptionID = responseDto.OptionID
            };
            _context.OptionResponses.Add(optionResponse);

            foreach (var textBoxResponseDto in responseDto.TextBoxResponses)
            {
                var textBoxResponse = new TextBoxResponses
                {
                    ResponseID = responses.ResponseID,
                    TextBoxID = textBoxResponseDto.TextBoxID,
                    TextValue = textBoxResponseDto.TextValue
                };
                _context.TextBoxResponses.Add(textBoxResponse);
            }

            await _context.SaveChangesAsync();
        }
    }
}
