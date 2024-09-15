using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

public class EntityService : IEntityService
{
    private readonly ApplicationDbContext _context;

    public EntityService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Tier
    public async Task<IEnumerable<Tier>> GetAllTiersAsync()
    {
        return await _context.Tier.ToListAsync();
    }

    public async Task<Tier> GetTierByIdAsync(int id)
    {
        return await _context.Tier.FindAsync(id);
    }

    public async Task AddTierAsync(Tier tier)
    {
        _context.Tier.Add(tier);
        await _context.SaveChangesAsync();
    }
    #endregion

    #region Category
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Category.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Category.FindAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Category.Add(category);
        await _context.SaveChangesAsync();
    }
    #endregion

    #region Domain
    public async Task<IEnumerable<Domain>> GetAllDomainsAsync()
    {
        return await _context.Domain.ToListAsync();
    }

    public async Task<Domain> GetDomainByIdAsync(int id)
    {
        return await _context.Domain.FindAsync(id);
    }

    public async Task AddDomainAsync(Domain domain)
    {
        _context.Domain.Add(domain);
        await _context.SaveChangesAsync();
    }
    #endregion

    #region Framework
    public async Task<IEnumerable<Framework>> GetAllFrameworksAsync()
    {
        return await _context.Framework.ToListAsync();
    }

    public async Task<Framework> GetFrameworkByIdAsync(int id)
    {
        return await _context.Framework.FindAsync(id);
    }

    public async Task AddFrameworkAsync(Framework framework)
    {
        _context.Framework.Add(framework);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<FrameworkDetails>> GetAllFrameworkDetailsAsync()
    {
        return await _context.FrameworkDetails.ToListAsync();
    }

    public async Task<FrameworkDetails> GetFrameworkDetailsByIdAsync(int id)
    {
        return await _context.FrameworkDetails.FindAsync(id);
    }

    public async Task AddFrameworkDetailsAsync(FrameworkDetails frameworkDetails)
    {
        _context.FrameworkDetails.Add(frameworkDetails);
        await _context.SaveChangesAsync();
    }
    #endregion

    #region UnitOfMeasurement
    public async Task<IEnumerable<UnitOfMeasurement>> GetAllUnitsOfMeasurementAsync()
    {
        return await _context.UnitOfMeasurement.ToListAsync();
    }

    public async Task<UnitOfMeasurement> GetUnitOfMeasurementByIdAsync(int id)
    {
        return await _context.UnitOfMeasurement.FindAsync(id);
    }

    public async Task AddUnitOfMeasurementAsync(UnitOfMeasurement unitOfMeasurement)
    {
        _context.UnitOfMeasurement.Add(unitOfMeasurement);
        await _context.SaveChangesAsync();
    }
    #endregion
    
    #region Status
    public async Task<IEnumerable<Status>> GetAllStatusesAsync()
    {
        return await _context.Status.ToListAsync();
    }

    public async Task<Status> GetStatusByIdAsync(int id)
    {
        return await _context.Status.FindAsync(id);
    }

    public async Task AddStatusAsync(Status status)
    {
        _context.Status.Add(status);
        await _context.SaveChangesAsync();
    }
    #endregion

}
