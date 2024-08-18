using Backend.Model;
using Backend.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly ApplicationDbContext _context;

        public QuestionnaireService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateQuestionnaireAsync(QuestionnaireDto questionnaireDto)
        {
            var questionnaire = new Questionnaire
            {
                Name = questionnaireDto.Name,
                Year = questionnaireDto.Year
            };

            _context.Questionnaires.Add(questionnaire);
            await _context.SaveChangesAsync();

            var questionQuestionnaires = new List<QuestionQuestionnaire>();
            foreach (var questionId in questionnaireDto.QuestionIDs)
            {
                questionQuestionnaires.Add(new QuestionQuestionnaire
                {
                    QuestionID = questionId,
                    QuestionnaireID = questionnaire.QuestionnaireID
                });
            }

            _context.QuestionQuestionnaire.AddRange(questionQuestionnaires);
            await _context.SaveChangesAsync();

            return questionnaire.QuestionnaireID;
        }

        public async Task<QuestionnaireWithQuestionsDto> GetQuestionsByQuestionnaireIdAsync(int questionnaireId)
        {
            var questionnaireData = await _context.QuestionQuestionnaire
                .Where(qq => qq.QuestionnaireID == questionnaireId)
                .Include(qq => qq.Questionnaire)
                .Select(qq => new QuestionnaireWithQuestionsDto
                {
                    QuestionnaireID = qq.Questionnaire.QuestionnaireID,
                    Name = qq.Questionnaire.Name,
                    Year = qq.Questionnaire.Year,
                    QuestionIDs = _context.QuestionQuestionnaire
                        .Where(q => q.QuestionnaireID == questionnaireId)
                        .Select(q => q.QuestionID)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return questionnaireData;
        }

        public async Task<List<QuestionnaireWithQuestionsDto>> GetAllQuestionnairesWithQuestionsAsync()
        {
            var allQuestionnaires = await _context.Questionnaires
                .Select(q => new QuestionnaireWithQuestionsDto
                {
                    QuestionnaireID = q.QuestionnaireID,
                    Name = q.Name,
                    Year = q.Year,
                    QuestionIDs = _context.QuestionQuestionnaire
                        .Where(qq => qq.QuestionnaireID == q.QuestionnaireID)
                        .Select(qq => qq.QuestionID)
                        .ToList()
                })
                .ToListAsync();

            return allQuestionnaires;
        }

    }
}
