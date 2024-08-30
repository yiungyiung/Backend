using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Backend.Model;

namespace Backend.Services
{
    public class ComplianceService
    {
        private readonly ApplicationDbContext _context;

        public ComplianceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, Dictionary<string, int>> GetComplianceDataForPastFiveYears()
        {
            var currentYear = DateTime.Now.Year;
            var fiveYearsAgo = currentYear - 4;

            var data = _context.QuestionnaireAssignments
                .Include(qa => qa.Status)
                .Include(qa => qa.Questionnaire)
                .Where(qa => qa.Questionnaire.Year >= fiveYearsAgo && qa.Questionnaire.Year <= currentYear)
                .GroupBy(qa => new { qa.Questionnaire.Year, qa.Status.StatusName })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Status = g.Key.StatusName,
                    Count = g.Count()
                })
                .ToList();

            var result = new Dictionary<string, Dictionary<string, int>>();

            for (int year = fiveYearsAgo; year <= currentYear; year++)
            {
                var yearData = data.Where(d => d.Year == year).ToList();
                var complied = yearData.FirstOrDefault(d => d.Status == "Complied")?.Count ?? 0;
                var notComplied = yearData.FirstOrDefault(d => d.Status == "Not-Complied")?.Count ?? 0;

                result[year.ToString()] = new Dictionary<string, int>
                {
                    { "Complied", complied },
                    { "Not-Complied", notComplied }
                };
            }

            return result;
        }
    }
}