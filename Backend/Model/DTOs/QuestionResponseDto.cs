﻿namespace Backend.Model.DTOs
{
    public class QuestionResponseDto
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string DomainName { get; set; }
        public int DomainID { get; set; }
        public List<QuestionOptionResponseDto> OptionResponses { get; set; } = new List<QuestionOptionResponseDto>();
        public List<QuestionTextBoxResponseDto> TextBoxResponses { get; set; } = new List<QuestionTextBoxResponseDto>();
        public List<QuestionFileUploadResponseDto> FileUploadResponses { get; set; } = new List<QuestionFileUploadResponseDto>();
    }

    public class QuestionOptionResponseDto
    {
        public int OptionID { get; set; }
        public string OptionText { get; set; }
    }

    public class QuestionTextBoxResponseDto
    {
        public int TextBoxID { get; set; }
        public string Label { get; set; }
        public string TextValue { get; set; }
    }

    public class QuestionnaireAssignmentResponseDto
    {
        public int AssignmentID { get; set; }
        public int QuestionnaireID { get; set; }
        public List<QuestionResponseDto> Questions { get; set; } = new List<QuestionResponseDto>();
    }
    public class QuestionFileUploadResponseDto  // New DTO for File Upload Responses
    {
        public int FileUploadID { get; set; }
        public string Label { get; set; }
        public string FilePath { get; set; }  // Path where the file is stored
        public string FileName { get; set; }  // Original file name
    }

}
