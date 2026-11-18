using Logistic.Domain.Entities;

namespace Logistic.Domain.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetCustomerByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Company> Items, int Total)> GetCustomersAsync(
        CustomerListFilter filter,
        CancellationToken cancellationToken);

    Task<bool> ExistsByTaxNumberAsync(string taxNumber, Guid? excludeCompanyId, CancellationToken cancellationToken);

    Task<bool> ExistsByNationalIdAsync(string nationalId, Guid? excludeCompanyId, CancellationToken cancellationToken);

    Task AddAsync(Company company, CancellationToken cancellationToken);

    Task UpdateAsync(Company company, CancellationToken cancellationToken);
}
