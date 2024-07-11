namespace Backend.Model
{
    public class VendorDto
    {
        public int VendorID { get; set; }

        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public int TierID { get; set; }
        public int UserID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryID { get; set; }
    }
}
