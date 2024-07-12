using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Model
{
   

    [Table("Vendor")]
    public class Vendor
    {
        public string VendorRegistration { get; set; }
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public int TierID { get; set; }
        public int UserID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryID { get; set; }
        public Tier Tier { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
    }

}
