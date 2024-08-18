public class DOptionDto
{
    public int OptionID { get; set; }
    public string OptionText { get; set; }
    public int OrderIndex { get; set; }
}

public class DTextboxDto
{
    public int TextBoxID { get; set; }
    public string Label { get; set; }
    public int OrderIndex { get; set; }
    public int UOMID { get; set; }
    public string UOMType { get; set; } // Additional context for UOM
}

public class DFileUploadDto
{
    public int FileUploadID { get; set; }
    public string Label { get; set; }
    public int OrderIndex { get; set; }
}

public class DetailedQuestionDto
{
    public int QuestionID { get; set; }
    public string QuestionText { get; set; }
    public string Description { get; set; }
    public int OrderIndex { get; set; }
    public int DomainID { get; set; }
    public string DomainName { get; set; } // Include Domain name for additional context
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } // Include Category name for additional context
    public int? ParentQuestionID { get; set; }

    public List<DOptionDto> Options { get; set; }
    public List<DTextboxDto> Textboxes { get; set; }
    public List<DFileUploadDto> FileUploads { get; set; }
}