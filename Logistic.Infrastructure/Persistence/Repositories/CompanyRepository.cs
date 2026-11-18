using Logistic.Domain.Entities;
using Logistic.Domain.Enums;
using Logistic.Domain.Repositories;
using Logistic.Infrastructure.Persistence.Context;
using Logistic.Infrastructure.Persistence.Repositories.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Logistic.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository : ICompanyRepository
{
    private readonly LogisticDbContext _dbContext;

    public CompanyRepository(LogisticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Company?> GetCustomerByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await BuildCustomerQuery()
            .FirstOrDefaultAsync(company => company.Id == id, cancellationToken);

        return entity is null ? null : CompanyEntityMap.ToDomain(entity);
    }

    public async Task<(IReadOnlyList<Company> Items, int Total)> GetCustomersAsync(
        CustomerListFilter filter,
        CancellationToken cancellationToken)
    {
        var query = BuildCustomerQuery();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(company =>
                company.Name.ToLower().Contains(search) ||
                (company.TaxNumber != null && company.TaxNumber.ToLower().Contains(search)) ||
                (company.NationalId != null && company.NationalId.ToLower().Contains(search)));
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(company => company.IsActive == filter.IsActive.Value);
        }

        var total = await query.CountAsync(cancellationToken);

        query = ApplySorting(query, filter);

        var page = Math.Max(filter.Page, 1);
        var pageSize = Math.Clamp(filter.PageSize, 1, 100);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items.Select(CompanyEntityMap.ToDomain).ToList(), total);
    }

    public Task<bool> ExistsByTaxNumberAsync(
        string taxNumber,
        Guid? excludeCompanyId,
        CancellationToken cancellationToken)
    {
        var normalized = taxNumber.Trim();
        return _dbContext.Companies.AnyAsync(
            company => company.TaxNumber == normalized && company.Id != excludeCompanyId,
            cancellationToken);
    }

    public Task<bool> ExistsByNationalIdAsync(
        string nationalId,
        Guid? excludeCompanyId,
        CancellationToken cancellationToken)
    {
        var normalized = nationalId.Trim();
        return _dbContext.Companies.AnyAsync(
            company => company.NationalId == normalized && company.Id != excludeCompanyId,
            cancellationToken);
    }

    public async Task AddAsync(Company company, CancellationToken cancellationToken)
    {
        var persistence = CompanyEntityMap.ToPersistence(company);
        await _dbContext.Companies.AddAsync(persistence, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Company company, CancellationToken cancellationToken)
    {
        var existing = await _dbContext.Companies
            .Include(item => item.CompanyRoles)
            .Include(item => item.CompanyContacts)
            .Include(item => item.CompanyNotes)
            .Include(item => item.CompanyBankAccounts)
            .FirstOrDefaultAsync(item => item.Id == company.Id, cancellationToken);

        if (existing is null)
        {
            return;
        }

        CompanyEntityMap.ToPersistence(company, existing);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<Scaffolded.Models.Company> BuildCustomerQuery()
    {
        return _dbContext.Companies
            .AsNoTracking()
            .Where(company => company.CompanyRoles.Any(role => role.Role == CompanyRoleType.Customer.ToString()))
            .Include(company => company.CompanyRoles)
            .Include(company => company.CompanyContacts)
            .Include(company => company.CompanyNotes)
            .Include(company => company.CompanyBankAccounts);
    }

    private static IQueryable<Scaffolded.Models.Company> ApplySorting(
        IQueryable<Scaffolded.Models.Company> query,
        CustomerListFilter filter)
    {
        return filter.SortBy.ToLowerInvariant() switch
        {
            "createddate" => filter.SortDescending
                ? query.OrderByDescending(company => company.CreatedDate)
                : query.OrderBy(company => company.CreatedDate),
            _ => filter.SortDescending
                ? query.OrderByDescending(company => company.Name)
                : query.OrderBy(company => company.Name)
        };
    }
}
