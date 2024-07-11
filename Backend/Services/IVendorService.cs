using Backend.Model;

namespace Backend.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor> AddVendorAsync(Vendor vendor);
        Task<Vendor> UpdateVendorAsync(Vendor vendor);
    }
}