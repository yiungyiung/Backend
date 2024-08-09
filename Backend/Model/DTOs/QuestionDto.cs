namespace Backend.Model.DTOs
{
    public class OptionDto
    {
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }
    }

    public class TextboxDto
    {
        public string Label { get; set; }
        public int OrderIndex { get; set; }
        public int UOMID { get; set; }
    }

    public class FileUploadDto
    {
        public string Label { get; set; }
        public int OrderIndex { get; set; }
    }

    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public int OrderIndex { get; set; }
        public int DomainID { get; set; }
        public int CategoryID { get; set; }
        public int? ParentQuestionID { get; set; }
        public List<OptionDto> Options { get; set; }
        public List<TextboxDto> Textboxes { get; set; }
        public List<FileUploadDto> FileUploads { get; set; }
        public List<int> FrameworkIDs { get; set; }
    }
}
