using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class VendorHierarchy
    {
        public int HierarchyID { get; set; }

        [Required]
        public int ParentVendorID { get; set; }

        [Required]
        public int ChildVendorID { get; set; }
    }
}