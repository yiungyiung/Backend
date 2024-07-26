using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminService _adminService;

        public VendorService(ApplicationDbContext context, IAdminService adminService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Vendor> AddVendorAsync(VendorDto vendorDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new User
                {
                    Email = vendorDto.User.Email,
                    Name = vendorDto.User.Name,
                    Contact_Number = vendorDto.User.Contact_Number,
                    RoleId = 4, // Assuming 4 is the RoleId for Vendor
                };

                var addedUser = await _adminService.AddUserAsync(user);

                var vendor = new Vendor
                {
                    VendorRegistration = vendorDto.VendorRegistration,
                    VendorName = vendorDto.VendorName,
                    VendorAddress = vendorDto.VendorAddress,
                    TierID = vendorDto.TierID,
                    UserID = addedUser.UserId,
                    RegistrationDate = DateTime.UtcNow,
                    CategoryID = vendorDto.CategoryID
                };

                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return vendor;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Vendor> GetVendorByIdAsync(int id)
        {
            var vendor = await _context.Vendors
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.VendorID == id);

            if (vendor == null)
            {
                throw new ArgumentException($"Vendor with id {id} not found.");
            }

            return vendor;
        }

        public async Task<Vendor> UpdateVendorAsync(Vendor vendor)
        {
            var existingVendor = await _context.Vendors.FindAsync(vendor.VendorID);
            if (existingVendor == null)
            {
                throw new ArgumentException($"Vendor with id {vendor.VendorID} not found.");
            }

            existingVendor.VendorRegistration = vendor.VendorRegistration;
            existingVendor.VendorName = vendor.VendorName;
            existingVendor.VendorAddress = vendor.VendorAddress;
            existingVendor.TierID = vendor.TierID;
            existingVendor.RegistrationDate = vendor.RegistrationDate;
            existingVendor.CategoryID = vendor.CategoryID;

            await _context.SaveChangesAsync();
            return existingVendor;
        }

        public async Task<Vendor> GetVendorHierarchyAsync(List<int> parentVendorIDs, Vendor currentVendor)
        {
            if (currentVendor == null)
            {
                throw new ArgumentNullException(nameof(currentVendor));
            }

            var parentVendors = await _context.Vendors
                .Where(v => parentVendorIDs.Contains(v.VendorID))
                .Include(v => v.Tier)
                .ToListAsync();

            var matchingVendors = new List<VendorHierarchy>();
            var mismatchedParentVendorIDs = new List<int>();

            foreach (var parentVendor in parentVendors)
            {
                if (parentVendor.TierID == currentVendor.TierID - 1)
                {
                    matchingVendors.Add(new VendorHierarchy
                    {
                        ParentVendorID = parentVendor.VendorID,
                        ChildVendorID = currentVendor.VendorID
                    });
                }
                else
                {
                    mismatchedParentVendorIDs.Add(parentVendor.VendorID);
                }
            }

            if (matchingVendors.Any())
            {
                _context.vendorHierarchy.AddRange(matchingVendors);
                await _context.SaveChangesAsync();
            }

            if (mismatchedParentVendorIDs.Any())
            {
                string mismatchedIDs = string.Join(", ", mismatchedParentVendorIDs);
                throw new ArgumentException($"One or more parent vendors' tiers do not match: {mismatchedIDs}", nameof(parentVendorIDs));
            }

            return currentVendor;
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Category.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Tier>> GetTiersAsync()
        {
            return await _context.Tier.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Vendor>> GetVendorsByTierAsync(int tierId)
        {
            return await _context.Vendors
                .Where(v => v.TierID == tierId)
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}