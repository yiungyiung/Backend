using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;

        public VendorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .ToListAsync();
        }
        public async Task<Vendor> AddVendorAsync(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task<Vendor> UpdateVendorAsync(Vendor vendor)
        {
            var existingVendor = await _context.Vendors.FindAsync(vendor.VendorID);
            if (existingVendor == null)
            {
                throw new ArgumentException($"Vendor with id {vendor.VendorID} not found.");
            }

            existingVendor.VendorName = vendor.VendorName;
            existingVendor.VendorAddress = vendor.VendorAddress;
            existingVendor.TierID = vendor.TierID;
            existingVendor.RegistrationDate = vendor.RegistrationDate;
            existingVendor.CategoryID = vendor.CategoryID;

            await _context.SaveChangesAsync();
            return existingVendor;
        }
    }

}
}
