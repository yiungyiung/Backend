using System.Collections.Generic;
using System.Linq;
using Backend.Model;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class QuestionnaireAssignmentService : IQuestionnaireAssignmentService
    {
        private readonly ApplicationDbContext _context;

        public QuestionnaireAssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateAssignments(QuestionnaireAssignmentDto dto)
        {
            foreach (var vendorID in dto.VendorIDs)
            {
                var assignment = new QuestionnaireAssignment
                {
                    VendorID = vendorID,
                    QuestionnaireID = dto.QuestionnaireID,
                    StatusID = dto.StatusID,
                    DueDate = dto.DueDate,
                    AssignmentDate = DateTime.Now,  // Automatically set
                    SubmissionDate = null,
                };

                _context.QuestionnaireAssignments.Add(assignment);
            }

            _context.SaveChanges();
        }

        public QuestionnaireAssignment GetAssignmentById(int assignmentId)
        {
            return _context.QuestionnaireAssignments.Find(assignmentId);
        }

        public IEnumerable<QuestionnaireAssignment> GetAssignmentsByVendorId(int vendorId)
        {
            return _context.QuestionnaireAssignments.Where(qa => qa.VendorID == vendorId).ToList();
        }

        public IEnumerable<QuestionnaireAssignment> GetAllAssignments()
        {
            return _context.QuestionnaireAssignments
                .Include(qa => qa.Vendor)
                .Include(qa => qa.Questionnaire)
                .Include(qa => qa.Status)
                .ToList(); 
        }
    }
}
