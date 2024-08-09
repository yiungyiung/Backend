using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class UnitOfMeasurement
    {
        [Key]
        public int UOMID { get; set; }  // Primary Key
        public string UOMType { get; set; }  // Type of Unit of Measurement

    }
}
