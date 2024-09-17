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

            // Get all distinct statuses from the database
            var allStatuses = _context.Status.Select(s => s.StatusName).ToList();

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

                // Initialize dictionary for the current year with all statuses
                var statusCounts = allStatuses.ToDictionary(status => status, status => 0);

                // Populate counts based on available data
                foreach (var entry in yearData)
                {
                    if (statusCounts.ContainsKey(entry.Status))
                    {
                        statusCounts[entry.Status] = entry.Count;
                    }
                }

                result[year.ToString()] = statusCounts;
            }

            return result;
        }

    }
}
