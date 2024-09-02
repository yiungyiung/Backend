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

        public async Task CreateAssignments(QuestionnaireAssignmentDto dto)
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

                await _context.QuestionnaireAssignments.AddAsync(assignment);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<QuestionnaireAssignment> GetAssignmentById(int assignmentId)
        {
            return await _context.QuestionnaireAssignments.FindAsync(assignmentId);
        }

        public async Task<List<QuestionnaireAssignment>> GetAssignmentsByVendorId(int vendorId)
        {
            return await _context.QuestionnaireAssignments
                .Where(qa => qa.VendorID == vendorId)
                .ToListAsync();
        }

        public async Task<List<QuestionnaireAssignment>> GetAllAssignments()
        {
            return await _context.QuestionnaireAssignments
                .Include(qa => qa.Vendor)
                .Include(qa => qa.Questionnaire)
                .Include(qa => qa.Status)
                .ToListAsync();
        }

        public async Task<List<QuestionnaireAssignment>> GetAssignmentsByQuestionnaireId(int questionnaireId)
        {
            return await _context.QuestionnaireAssignments
                .Include(qa => qa.Vendor)
                .Include(qa => qa.Questionnaire)
                .Include(qa => qa.Status)
                .Where(qa => qa.QuestionnaireID == questionnaireId)
                .ToListAsync();
        }
    }
}