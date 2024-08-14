namespace Backend.Model.DTOs
{
    public class QuestionnaireAssignmentDto
    {
        public List<int> VendorIDs { get; set; }
        public int QuestionnaireID { get; set; }
        public int StatusID { get; set; }
        public DateTime DueDate { get; set; }
    }

}
