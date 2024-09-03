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
        private readonly IEmailService _emailService;
        public QuestionnaireAssignmentService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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
                var vendor = await _context.Vendors
            .FirstOrDefaultAsync(v => v.VendorID == vendorID);

                if (vendor != null)
                {
                    // Fetch the user entity using the UserID from the vendor
                    var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserId == vendor.UserID);

                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        // Send notification email to the user's email
                        await SendAssignmentNotificationEmailAsync(user.Email, dto.QuestionnaireID);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        private async Task SendAssignmentNotificationEmailAsync(string email, int questionnaireId)
        {
            string subject = "New Assignment Notification";
            string body = $"A new assignment has been assigned to you for Questionnaire ID: {questionnaireId}. Please review and complete it by the due date.";
            await _emailService.SendEmailAsync(email, subject, body);
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