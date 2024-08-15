namespace Backend.Model.DTOs
{
    public class VendorHierarchyDto
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public int TierID { get; set; }
        public List<VendorHierarchyDto> Children { get; set; } = new List<VendorHierarchyDto>();
    }
}
