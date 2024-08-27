namespace Backend.Model.DTOs
{
    public class ResponseDto
    {
        public int AssignmentID { get; set; }
        public int QuestionID { get; set; }
        public int? OptionID { get; set; }
        public List<TextBoxResponseDto> TextBoxResponses { get; set; } = new List<TextBoxResponseDto>();
    }

    public class TextBoxResponseDto
    {
        public int TextBoxID { get; set; }
        public string TextValue { get; set; }
    }
}
