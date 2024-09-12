using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Authorize(Roles = "Admin,Manager,Analyst,Vendor")]
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IEntityService _entityService;

        public EntityController(IEntityService entityService)
        {
            _entityService = entityService;
        }

        #region Tier
        [HttpGet("tiers")]
        public async Task<ActionResult<IEnumerable<Tier>>> GetAllTiers()
        {
            var tiers = await _entityService.GetAllTiersAsync();
            return Ok(tiers);
        }

        [HttpGet("tiers/{id}")]
        public async Task<ActionResult<Tier>> GetTierById(int id)
        {
            var tier = await _entityService.GetTierByIdAsync(id);
            if (tier == null) return NotFound();
            return Ok(tier);
        }

        [HttpPost("tiers")]
        public async Task<ActionResult> AddTier(Tier tier)
        {
            await _entityService.AddTierAsync(tier);
            return CreatedAtAction(nameof(GetTierById), new { id = tier.TierId }, tier);
        }
        #endregion

        #region Category
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _entityService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _entityService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("categories")]
        public async Task<ActionResult> AddCategory(Category category)
        {
            await _entityService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryID }, category);
        }
        #endregion

        #region Domain
        [HttpGet("domains")]
        public async Task<ActionResult<IEnumerable<Domain>>> GetAllDomains()
        {
            var domains = await _entityService.GetAllDomainsAsync();
            return Ok(domains);
        }

        [HttpGet("domains/{id}")]
        public async Task<ActionResult<Domain>> GetDomainById(int id)
        {
            var domain = await _entityService.GetDomainByIdAsync(id);
            if (domain == null) return NotFound();
            return Ok(domain);
        }

        [HttpPost("domains")]
        public async Task<ActionResult> AddDomain(Domain domain)
        {
            await _entityService.AddDomainAsync(domain);
            return CreatedAtAction(nameof(GetDomainById), new { id = domain.DomainID }, domain);
        }
        #endregion

        #region Framework
        [HttpGet("frameworks")]
        public async Task<ActionResult<IEnumerable<Framework>>> GetAllFrameworks()
        {
            var frameworks = await _entityService.GetAllFrameworksAsync();
            return Ok(frameworks);
        }

        [HttpGet("frameworks/{id}")]
        public async Task<ActionResult<Framework>> GetFrameworkById(int id)
        {
            var framework = await _entityService.GetFrameworkByIdAsync(id);
            if (framework == null) return NotFound();
            return Ok(framework);
        }

        [HttpPost("frameworks")]
        public async Task<ActionResult> AddFramework(Framework framework)
        {
            await _entityService.AddFrameworkAsync(framework);
            return CreatedAtAction(nameof(GetFrameworkById), new { id = framework.FrameworkID }, framework);
        }
        #endregion

        #region UnitOfMeasurement
        [HttpGet("unitsOfMeasurement")]
        public async Task<ActionResult<IEnumerable<UnitOfMeasurement>>> GetAllUnitsOfMeasurement()
        {
            var units = await _entityService.GetAllUnitsOfMeasurementAsync();
            return Ok(units);
        }

        [HttpGet("unitsOfMeasurement/{id}")]
        public async Task<ActionResult<UnitOfMeasurement>> GetUnitOfMeasurementById(int id)
        {
            var unit = await _entityService.GetUnitOfMeasurementByIdAsync(id);
            if (unit == null) return NotFound();
            return Ok(unit);
        }

        [HttpPost("unitsOfMeasurement")]
        public async Task<ActionResult> AddUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
        {
            await _entityService.AddUnitOfMeasurementAsync(unitOfMeasurement);
            return CreatedAtAction(nameof(GetUnitOfMeasurementById), new { id = unitOfMeasurement.UOMID }, unitOfMeasurement);
        }
        #endregion

        #region Status
        [HttpGet("statuses")]
        public async Task<ActionResult<IEnumerable<Status>>> GetAllStatuses()
        {
            var statuses = await _entityService.GetAllStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("statuses/{id}")]
        public async Task<ActionResult<Status>> GetStatusById(int id)
        {
            var status = await _entityService.GetStatusByIdAsync(id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        [HttpPost("statuses")]
        public async Task<ActionResult> AddStatus(Status status)
        {
            await _entityService.AddStatusAsync(status);
            return CreatedAtAction(nameof(GetStatusById), new { id = status.StatusID }, status);
        }
        #endregion

    }
}
