using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class QuestionFramework
    {
        [Key]   
        
        public int QuestionFrameworkID { get; set; }
        public int FrameworkID { get; set; }
        public int QuestionID { get; set; }

        public Framework Framework { get; set; }
        public Question Question { get; set; }
    }
}
