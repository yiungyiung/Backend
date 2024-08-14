using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model
{
    public class QuestionnaireAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        public int VendorID { get; set; }
        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }

        public int QuestionnaireID { get; set; }
        [ForeignKey("QuestionnaireID")]
        public Questionnaire Questionnaire { get; set; }

        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public Status Status { get; set; }

        public DateTime AssignmentDate { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }
    }
}
