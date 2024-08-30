using Backend.Model;
using Backend.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor> AddVendorAsync(VendorDto vendorDto);
        Task<Vendor> GetVendorByIdAsync(int id);
        Task<Vendor> UpdateVendorAsync(Vendor vendor);
        Task<Vendor> GetVendorHierarchyAsync(List<int> parentVendorIDs, Vendor currentvendorID);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Tier>> GetTiersAsync();

        Task<IEnumerable<Vendor>> GetVendorsByTierAsync(int tierId);
        Task<int?> GetVendorIdByUserIdAsync(int userId);
        Task<List<VendorsByCategoryDto>> GetVendorsGroupedByCategoryAsync();
    }
}
