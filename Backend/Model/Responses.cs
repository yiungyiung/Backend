using Azure;

namespace Backend.Model
{
    public class Responses
    {
        public int ResponseID { get; set; }
        public int AssignmentID { get; set; }
        public int QuestionID { get; set; }

        public QuestionnaireAssignment Assignment { get; set; }
        public Question Question { get; set; }
    }

    public class OptionResponses
    {
        public int OptionResponseID { get; set; }
        public int ResponseID { get; set; }
        public int OptionID { get; set; }

        public Responses Response { get; set; }
        public Option Option { get; set; }
    }

    public class TextBoxResponses
    {
        public int TextBoxResponseID { get; set; }
        public int ResponseID { get; set; }
        public int TextBoxID { get; set; }
        public string TextValue { get; set; }

        public Responses Response { get; set; }
        public Textbox TextBox { get; set; }
    }
}
