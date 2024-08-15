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
    #endregion
}
