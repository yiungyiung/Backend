using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;

public interface IEntityService
{
    #region Tier
    Task<IEnumerable<Tier>> GetAllTiersAsync();
    Task<Tier> GetTierByIdAsync(int id);
    Task AddTierAsync(Tier tier);
    #endregion

    #region Category
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(Category category);
    #endregion

    #region Domain
    Task<IEnumerable<Domain>> GetAllDomainsAsync();
    Task<Domain> GetDomainByIdAsync(int id);
    Task AddDomainAsync(Domain domain);
    #endregion

    #region Framework
    Task<IEnumerable<Framework>> GetAllFrameworksAsync();
    Task<Framework> GetFrameworkByIdAsync(int id);
    Task AddFrameworkAsync(Framework framework);
    Task<IEnumerable<FrameworkDetails>> GetAllFrameworkDetailsAsync();
    #endregion

    #region UnitOfMeasurement
    Task<IEnumerable<UnitOfMeasurement>> GetAllUnitsOfMeasurementAsync();
    Task<UnitOfMeasurement> GetUnitOfMeasurementByIdAsync(int id);
    Task AddUnitOfMeasurementAsync(UnitOfMeasurement unitOfMeasurement);
    #endregion

 Task AddStatusAsync(Status status);
Task<Status> GetStatusByIdAsync(int id);
Task<IEnumerable<Status>> GetAllStatusesAsync();

}
