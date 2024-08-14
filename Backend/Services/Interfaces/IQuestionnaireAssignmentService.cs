using System.Collections.Generic;
using Backend.Model;
using Backend.Model.DTOs;

namespace Backend.Services.Interfaces
{
    public interface IQuestionnaireAssignmentService
    {
        void CreateAssignments(QuestionnaireAssignmentDto dto);
        QuestionnaireAssignment GetAssignmentById(int assignmentId);
        IEnumerable<QuestionnaireAssignment> GetAssignmentsByVendorId(int vendorId);
    }
}
