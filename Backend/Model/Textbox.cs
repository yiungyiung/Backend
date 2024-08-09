namespace Backend.Model
{

    public class Textbox
    {
        public int TextBoxID { get; set; }
        public int QuestionID { get; set; }
        public string Label { get; set; }
        public int OrderIndex { get; set; }
        public int UOMID { get; set; }

        public Question Question { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
    }
}
