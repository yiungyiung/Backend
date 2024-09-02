using System.Collections.Generic;
using Backend.Model;
using Backend.Model.DTOs;

namespace Backend.Services.Interfaces
{
    public interface IQuestionnaireAssignmentService
    {
        Task CreateAssignments(QuestionnaireAssignmentDto dto);
        Task<QuestionnaireAssignment> GetAssignmentById(int assignmentId);
        Task<List<QuestionnaireAssignment>> GetAssignmentsByVendorId(int vendorId);
        Task<List<QuestionnaireAssignment>> GetAllAssignments();
        Task<List<QuestionnaireAssignment>> GetAssignmentsByQuestionnaireId(int questionnaireId);
    }
}
