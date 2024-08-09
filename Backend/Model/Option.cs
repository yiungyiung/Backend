namespace Backend.Model
{
    public class Option
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }

        public Question Question { get; set; }
    }

}
