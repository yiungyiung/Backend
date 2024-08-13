using Backend.Model.DTOs;

namespace Backend.Services
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(QuestionnaireDto questionnaireDto);
    }
}
