namespace Backend.Model.DTOs
{
    public class ResponseDto
    {
        public int AssignmentID { get; set; }
        public int QuestionID { get; set; }
        public int? OptionID { get; set; }
        public List<TextBoxResponseDto> TextBoxResponses { get; set; } = new List<TextBoxResponseDto>();
        public List<FileUploadResponseDto> FileUploadResponses { get; set; } = new List<FileUploadResponseDto>();
    }

    public class TextBoxResponseDto
    {
        public int TextBoxID { get; set; }
        public string TextValue { get; set; }
    }

    public class FileUploadResponseDto  // New DTO for File Upload Responses
    {
        public int FileUploadID { get; set; }
        public string FilePath { get; set; }  // Path where the file is stored
        public string FileName { get; set; }  // Original file name
        public IFormFile? File { get; set; }
    }
}
