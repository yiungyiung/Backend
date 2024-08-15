using Backend.Model.DTOs;

namespace Backend.Services.Interfaces
{
    public interface IVendorHierarchy
    {
        Task<List<VendorHierarchyDto>> GetVendorHierarchyAsync();

    }
}
