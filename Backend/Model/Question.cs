namespace Backend.Model
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int? ParentQuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int DomainID { get; set; }
        public int CategoryID { get; set; }

        public Domain Domain { get; set; }
        public Category Category { get; set; }

    }
    
}
