using Backend.Model.DTOs;

namespace Backend.Services
{
    public interface IQuestionnaireService
    {
        Task<int> CreateQuestionnaireAsync(QuestionnaireDto questionnaireDto);
        Task<QuestionnaireWithQuestionsDto> GetQuestionsByQuestionnaireIdAsync(int questionnaireId);
        Task<List<QuestionnaireWithQuestionsDto>> GetAllQuestionnairesWithQuestionsAsync();


    }
}
