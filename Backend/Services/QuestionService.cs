using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Question> AddQuestionAsync(QuestionDto questionDto)
        {
            var question = new Question
            {
                QuestionText = questionDto.QuestionText,
                Description = questionDto.Description,
                OrderIndex = questionDto.OrderIndex,
                DomainID = questionDto.DomainID,
                CategoryID = questionDto.CategoryID,
                ParentQuestionID = questionDto.ParentQuestionID
            };

            // Add question to the database
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // Add options, textboxes, and frameworks
            await AddOptionsAsync(question.QuestionID, questionDto.Options);
            await AddTextboxesAsync(question.QuestionID, questionDto.Textboxes);
            await AddFrameworksAsync(question.QuestionID, questionDto.FrameworkIDs);

            return question;
        }

        private async Task AddOptionsAsync(int questionID, IEnumerable<OptionDto> options)
        {
            foreach (var option in options)
            {
                _context.Options.Add(new Option
                {
                    QuestionID = questionID,
                    OptionText = option.OptionText,
                    OrderIndex = option.OrderIndex
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task AddTextboxesAsync(int questionID, IEnumerable<TextboxDto> textboxes)
        {
            foreach (var textbox in textboxes)
            {
                _context.Textboxes.Add(new Textbox
                {
                    QuestionID = questionID,
                    Label = textbox.Label,
                    OrderIndex = textbox.OrderIndex,
                    UOMID = (int) textbox.UOMID
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task AddFrameworksAsync(int questionID, IEnumerable<int> frameworkIDs)
        {
            foreach (var frameworkID in frameworkIDs)
            {
                _context.QuestionFramework.Add(new QuestionFramework
                {
                    FrameworkID = frameworkID,
                    QuestionID = questionID
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
