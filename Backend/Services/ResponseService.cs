﻿using Azure;
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
            if (responseDto.FileUploadResponses != null && responseDto.FileUploadResponses.Count > 0)
            {
                foreach (var fileUploadResponseDto in responseDto.FileUploadResponses)
                {
                    var fileUploadResponse = new FileUploadResponses
                    {
                        ResponseID = responses.ResponseID,
                        FileUploadID = fileUploadResponseDto.FileUploadID,
                        FilePath = fileUploadResponseDto.FilePath,
                        FileName = fileUploadResponseDto.FileName
                    };
                    _context.FileUploadResponses.Add(fileUploadResponse);
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
                if (responseDto.FileUploadResponses != null && responseDto.FileUploadResponses.Count > 0)
                {
                    foreach (var fileUploadResponseDto in responseDto.FileUploadResponses)
                    {
                        var fileUploadResponse = new FileUploadResponses
                        {
                            ResponseID = responsesEntity.ResponseID,
                            FileUploadID = fileUploadResponseDto.FileUploadID,
                            FilePath = fileUploadResponseDto.FilePath,
                            FileName = fileUploadResponseDto.FileName
                        };
                        _context.FileUploadResponses.Add(fileUploadResponse);
                    }
                }
            }

            await _context.SaveChangesAsync();

            // Update the StatusID of the QuestionnaireAssignment to 1
            var assignment = await _context.QuestionnaireAssignments
                .FirstOrDefaultAsync(a => a.AssignmentID == assignmentID);
            if (assignment != null)
            {
                assignment.StatusID = 1;
                assignment.SubmissionDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<QuestionnaireAssignmentResponseDto> GetResponsesForAssignmentIdAsync(int assignmentId)
        {
            // Retrieve the assignment
            var assignment = await _context.QuestionnaireAssignments
                .FirstOrDefaultAsync(a => a.AssignmentID == assignmentId);

            if (assignment == null)
                return null;

            // Create the DTO to hold the results
            var assignmentResponseDto = new QuestionnaireAssignmentResponseDto
            {
                AssignmentID = assignmentId,
                QuestionnaireID = assignment.QuestionnaireID,
                Questions = new List<QuestionResponseDto>()
            };

            // Retrieve the responses for this assignment
            var responses = await _context.Responses
                .Where(r => r.AssignmentID == assignmentId)
                .Include(r => r.Question)
                .ThenInclude(q => q.Domain) // Include Domain information
                .ToListAsync();

            foreach (var response in responses)
            {
                var questionDto = new QuestionResponseDto
                {
                    QuestionID = response.QuestionID,
                    QuestionText = response.Question.QuestionText,
                    DomainName = response.Question.Domain?.DomainName, // Set DomainName
                    DomainID = response.Question.Domain?.DomainID ?? default, // Set DomainID with a default value
                    OptionResponses = await GetOptionResponsesAsync(response.ResponseID),
                    TextBoxResponses = await _context.TextBoxResponses
                        .Where(tr => tr.ResponseID == response.ResponseID)
                        .Select(tr => new QuestionTextBoxResponseDto
                        {
                            TextBoxID = tr.TextBoxID,
                            Label = tr.TextBox.Label,
                            TextValue = tr.TextValue
                        }).ToListAsync(),
                    FileUploadResponses = await _context.FileUploadResponses
                        .Where(fu => fu.ResponseID == response.ResponseID)
                        .Select(fu => new QuestionFileUploadResponseDto
                        {
                            FileUploadID = fu.FileUploadID,
                            Label = fu.FileUpload.Label,
                            FilePath = fu.FilePath,
                            FileName = fu.FileName
                        }).ToListAsync()
                };

                assignmentResponseDto.Questions.Add(questionDto);
            }

            return assignmentResponseDto;
        }

        public async Task<List<QuestionnaireAssignmentResponseDto>> GetAllResponsesForQuestionnaireIdAsync(int questionnaireId)
        {
            var allAssignmentResponses = new List<QuestionnaireAssignmentResponseDto>();

            var assignments = await _context.QuestionnaireAssignments
                .Where(qa => qa.QuestionnaireID == questionnaireId && qa.StatusID == 1)
                .ToListAsync();

            foreach (var assignment in assignments)
            {
                var assignmentResponse = await GetResponsesForAssignmentIdAsync(assignment.AssignmentID);
                if (assignmentResponse != null)
                {
                    allAssignmentResponses.Add(assignmentResponse);
                }
            }

            return allAssignmentResponses;
        }

        public async Task<QuestionResponseDto> GetResponseForAssignmentAndQuestionAsync(int assignmentId, int questionId)
        {
            // Retrieve the specific response based on AssignmentID and QuestionID
            var response = await _context.Responses
                .Where(r => r.AssignmentID == assignmentId && r.QuestionID == questionId)
                .Include(r => r.Question)
                .ThenInclude(q => q.Domain) // Include Domain information
                .FirstOrDefaultAsync();

            if (response == null)
                return null;

            // Create the DTO to hold the response details
            var questionResponseDto = new QuestionResponseDto
            {
                QuestionID = response.QuestionID,
                QuestionText = response.Question.QuestionText,
                DomainName = response.Question.Domain?.DomainName, // Set DomainName
                DomainID = response.Question.Domain?.DomainID ?? default, // Set DomainID with a default value

                // Retrieve and set the OptionResponses
                OptionResponses = await GetOptionResponsesAsync(response.ResponseID),

                // Retrieve and set the TextBoxResponses
                TextBoxResponses = await _context.TextBoxResponses
                    .Where(tr => tr.ResponseID == response.ResponseID)
                    .Select(tr => new QuestionTextBoxResponseDto
                    {
                        TextBoxID = tr.TextBoxID,
                        Label = tr.TextBox.Label,
                        TextValue = tr.TextValue
                    }).ToListAsync(),
                FileUploadResponses = await _context.FileUploadResponses
                    .Where(fu => fu.ResponseID == response.ResponseID)
                    .Select(fu => new QuestionFileUploadResponseDto
                    {
                        FileUploadID = fu.FileUploadID,
                        Label = fu.FileUpload.Label,
                        FilePath = fu.FilePath,
                        FileName = fu.FileName
                    }).ToListAsync()
            };

            return questionResponseDto;
        }

        private async Task<List<QuestionOptionResponseDto>> GetOptionResponsesAsync(int responseId)
        {
            return await _context.OptionResponses
                .Where(or => or.ResponseID == responseId)
                .Select(or => new QuestionOptionResponseDto
                {
                    OptionID = or.OptionID,
                    OptionText = or.Option.OptionText
                })
                .ToListAsync();
        }

        private async Task<List<QuestionTextBoxResponseDto>> GetTextBoxResponsesAsync(int responseId)
        {
            return await _context.TextBoxResponses
                .Where(tr => tr.ResponseID == responseId)
                .Select(tr => new QuestionTextBoxResponseDto
                {
                    TextBoxID = tr.TextBoxID,
                    Label = tr.TextBox.Label,
                    TextValue = tr.TextValue
                })
                .ToListAsync();
        }
    }
}
