namespace Backend.Model.DTOs
{
    public class QuestionQuestionnaireDto
    {
        public int QuestionID { get; set; }
        public int QuestionnaireID { get; set; }
    }

    public class QuestionnaireWithQuestionsDto
    {
        public int QuestionnaireID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<int> QuestionIDs { get; set; }
    }
}
