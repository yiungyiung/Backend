namespace Backend.Model
{
    public class QuestionQuestionnaire
    {
        public int QuestionQuestionnaireID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionnaireID { get; set; }

        public Question Question { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}
