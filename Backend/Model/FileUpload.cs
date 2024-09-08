namespace Backend.Model
{
    public class FileUpload
    {
        public int FileUploadID { get; set; }      // Primary Key
        public int QuestionID { get; set; }        // Foreign Key to Question
        public string Label { get; set; }
        public int OrderIndex { get; set; }

        public Question Question { get; set; }     // Navigation property for Question
    }
}
