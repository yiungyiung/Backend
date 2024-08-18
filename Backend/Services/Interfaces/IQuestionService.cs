using Backend.Model;
using Backend.Model.DTOs;

namespace Backend.Services
{
    public interface IQuestionService
    {
        Task<Question> AddQuestionAsync(QuestionDto questionDto);
        Task<DetailedQuestionDto> GetQuestionByIdAsync(int questionID);
    }
}
