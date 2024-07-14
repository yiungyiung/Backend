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
            _context = context;
            _adminService = adminService;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .ToListAsync();
        }

        public async Task<Vendor> AddVendorAsync(VendorDto vendorDto)
        {
            try
            {
                // Create a new user for the vendor
                var user = new User
                {
                    Email = vendorDto.User.Email,
                    Name = vendorDto.User.Name,
                    Contact_Number = vendorDto.User.Contact_Number,
                    RoleId = 4, // Assuming 4 is the RoleId for Vendor
                    PasswordHash = GenerateRandomPassword()
                };

                var addedUser = await _adminService.AddUserAsync(user);

                // Check if user is added successfully
                if (addedUser == null)
                {
                    throw new Exception("Failed to add user.");
                }

                // Create the vendor using the created user
                var vendor = new Vendor
                {
                    VendorRegistration = vendorDto.VendorRegistration,
                    VendorName = vendorDto.VendorName,
                    VendorAddress = vendorDto.VendorAddress,
                    TierID = vendorDto.TierID,
                    UserID = addedUser.UserId,
                    RegistrationDate = DateTime.Now,
                    CategoryID = vendorDto.CategoryID
                };

                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();

                // Send welcome email to the user
                await _adminService.SendWelcomeEmailAsync(addedUser.Email, user.PasswordHash);

                return vendor;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in AddVendorAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Vendor> GetVendorByIdAsync(int id)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetVendorByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Vendor> UpdateVendorAsync(Vendor vendor)
        {
            try
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
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateVendorAsync: {ex.Message}");
                throw;
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return password;
        }

        public async Task<Vendor> GetVendorHierarchyAsync(List<int> parentVendorIDs, Vendor? currentVendor)
        {
            if (currentVendor == null)
            {
                throw new ArgumentNullException(nameof(currentVendor), "Current vendor cannot be null.");
            }

            if (parentVendorIDs == null || parentVendorIDs.Count == 0)
            {
                throw new ArgumentException("Parent vendor IDs cannot be null or empty.", nameof(parentVendorIDs));
            }

            try
            {
                int vendorID = currentVendor.VendorID;

                foreach (var parentVendorID in parentVendorIDs)
                {
                    var parentVendor = await _context.Vendors
                        .Include(v => v.Tier)
                        .Include(v => v.Category)
                        .Include(v => v.User)
                        .FirstOrDefaultAsync(v => v.VendorID == parentVendorID);
                    if (parentVendor == null)
                    {
                        continue;
                    }
                    if (parentVendor.TierID == currentVendor.TierID - 1)
                    {
                        var vendorHierarchy = new VendorHierarchy
                        {
                            ParentVendorID = parentVendorID,
                            ChildVendorID = currentVendor.VendorID
                        };
                        _context.vendorHierarchy.Add(vendorHierarchy);
                    }
                    else
                    {
                        throw new ArgumentException("Tier of Parent vendor does not match.", nameof(parentVendorIDs));
                    }

                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("An error occurred while updating the database. See inner exception for details.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred. See inner exception for details.", ex);
            }

            return currentVendor;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<IEnumerable<Tier>> GetTiersAsync()
        {
            return await _context.Tier.ToListAsync();
        }

        public async Task<IEnumerable<Vendor>> GetVendorsByTierAsync(int tierId)
        {
            return await _context.Vendors
                .Where(v => v.TierID == tierId)
                .Include(v => v.Tier)
                .Include(v => v.Category)
                .Include(v => v.User)
                .ToListAsync();
        }


    }
}
