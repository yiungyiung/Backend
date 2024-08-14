using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }
    }
}
