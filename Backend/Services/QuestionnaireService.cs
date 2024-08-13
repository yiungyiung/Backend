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
    }
}
