namespace Backend.Model.DTOs
{
    public class QuestionnaireDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public List<int> QuestionIDs { get; set; }
    }
}
