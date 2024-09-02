using Backend.Model.DTOs;

namespace Backend.Services.Interfaces
{
    public interface IResponseService
    {
        Task SaveResponseAsync(ResponseDto responseDto);
        Task SaveAllResponsesAsync(List<ResponseDto> responses);
        Task<QuestionnaireAssignmentResponseDto> GetResponsesForAssignmentIdAsync(int assignmentId);
        Task<List<QuestionnaireAssignmentResponseDto>> GetAllResponsesForQuestionnaireIdAsync(int questionnaireId);
        Task<QuestionResponseDto> GetResponseForAssignmentAndQuestionAsync(int assignmentId, int questionId);
    }
    
}
