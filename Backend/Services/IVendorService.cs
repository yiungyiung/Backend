using Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor> AddVendorAsync(VendorDto vendorDto);
        Task<Vendor> GetVendorByIdAsync(int id);
        Task<Vendor> UpdateVendorAsync(Vendor vendor);
    }
}
