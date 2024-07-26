using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Tier
    {
        public int TierId { get; set; }

        [Required]
        [StringLength(50)]
        public string TierName { get; set; }
    }
}