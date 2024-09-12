namespace Backend.Model.DTOs
{
    public class AssignmentStatisticsDto
    {
        public int TotalAssignments { get; set; }
        public List<StatusCountDto> AssignmentsByStatus { get; set; }
        public int LateSubmissions { get; set; }
    }

    // DTO to hold status-wise counts
    public class StatusCountDto
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
    }
}
