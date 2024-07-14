using Backend.Model;

public class VendorHierarchy
{
    public int HierarchyID { get; set; } 
    public int ParentVendorID { get; set; }
    public int ChildVendorID { get; set; }

}