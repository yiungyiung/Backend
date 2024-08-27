using Azure;
using Backend.Model.DTOs;
using Backend.Model;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            // Create a response entry in the database
            var responses = new Responses
            {
                AssignmentID = responseDto.AssignmentID,
                QuestionID = responseDto.QuestionID
            };

            _context.Responses.Add(responses);
            await _context.SaveChangesAsync();

            // Check if OptionID is not null before adding an OptionResponse
            if (responseDto.OptionID.HasValue)
            {
                var optionResponse = new OptionResponses
                {
                    ResponseID = responses.ResponseID,
                    OptionID = responseDto.OptionID.Value
                };
                _context.OptionResponses.Add(optionResponse);
            }

            // Check if there are any TextBoxResponses before adding them
            if (responseDto.TextBoxResponses != null && responseDto.TextBoxResponses.Count > 0)
            {
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
            }

            await _context.SaveChangesAsync();
        }
        public async Task SaveAllResponsesAsync(List<ResponseDto> responses)
        {
            // Loop through each responseDto and save it to the database
            if (responses == null || responses.Count == 0)
            {
                return;
            }

            // Extract the AssignmentID from the first response (assuming all responses share the same AssignmentID)
            int assignmentID = responses[0].AssignmentID;
            foreach (var responseDto in responses)
            {
                var responsesEntity = new Responses
                {
                    AssignmentID = responseDto.AssignmentID,
                    QuestionID = responseDto.QuestionID
                };

                _context.Responses.Add(responsesEntity);
                await _context.SaveChangesAsync();

                // Save OptionResponse if present
                if (responseDto.OptionID.HasValue)
                {
                    var optionResponse = new OptionResponses
                    {
                        ResponseID = responsesEntity.ResponseID,
                        OptionID = responseDto.OptionID.Value
                    };
                    _context.OptionResponses.Add(optionResponse);
                }

                // Save TextBoxResponses if present
                if (responseDto.TextBoxResponses != null && responseDto.TextBoxResponses.Count > 0)
                {
                    foreach (var textBoxResponseDto in responseDto.TextBoxResponses)
                    {
                        var textBoxResponse = new TextBoxResponses
                        {
                            ResponseID = responsesEntity.ResponseID,
                            TextBoxID = textBoxResponseDto.TextBoxID,
                            TextValue = textBoxResponseDto.TextValue
                        };
                        _context.TextBoxResponses.Add(textBoxResponse);
                    }
                }
            }

            await _context.SaveChangesAsync();
            // Update the StatusID of the QuestionnaireAssignment to 1
            var assignment = await _context.QuestionnaireAssignments
                .FirstOrDefaultAsync(a => a.AssignmentID == assignmentID);
            Console.WriteLine(assignment.StatusID + "yash");
            if (assignment != null)
            {
                assignment.StatusID = 1;
                assignment.SubmissionDate = DateTime.Now;
                await _context.SaveChangesAsync();
                Console.WriteLine(assignment.StatusID+"yash");
            }
        }
    }
}
