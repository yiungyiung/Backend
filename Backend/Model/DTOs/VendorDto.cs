namespace Backend.Model.DTOs
{
    public class VendorDto
    {
        public string VendorRegistration { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public int TierID { get; set; }
        public UserDto User { get; set; }
        public int CategoryID { get; set; }
        public List<int> parentVendorIDs { get; set; }
    }
}
