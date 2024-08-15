using Backend.Model;
using Backend.Model.DTOs;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class VendorHierarchyService : IVendorHierarchy
    {
        private readonly ApplicationDbContext _context;

        public VendorHierarchyService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<VendorHierarchyDto>> GetVendorHierarchyAsync()
        {
            var vendors = await _context.Vendors.ToListAsync();
            var hierarchies = await _context.vendorHierarchy.ToListAsync();

            // Build a dictionary of vendors
            var vendorDict = vendors.ToDictionary(v => v.VendorID, v => new VendorHierarchyDto
            {
                VendorID = v.VendorID,
                VendorName = v.VendorName,
                TierID = v.TierID,
                Children = new List<VendorHierarchyDto>()
            });

            // Build the hierarchy
            foreach (var hierarchy in hierarchies)
            {
                if (vendorDict.ContainsKey(hierarchy.ParentVendorID) && vendorDict.ContainsKey(hierarchy.ChildVendorID))
                {
                    var parent = vendorDict[hierarchy.ParentVendorID];
                    var child = vendorDict[hierarchy.ChildVendorID];
                    parent.Children.Add(child);
                }
            }

            // Return the root vendors (vendors without parents)
            return vendorDict.Values.Where(v => !hierarchies.Any(h => h.ChildVendorID == v.VendorID)).ToList();
        }
    }
}
