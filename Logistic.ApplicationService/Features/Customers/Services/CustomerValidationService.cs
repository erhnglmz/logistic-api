using Logistic.Domain.Enums;
using Logistic.Domain.Exceptions;
using Logistic.Domain.Repositories;

namespace Logistic.ApplicationService.Features.Customers.Services;

public sealed class CustomerValidationService
{
    private readonly ICompanyRepository _companyRepository;

    public CustomerValidationService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public CompanyType ParseCompanyType(string value)
    {
        if (!Enum.TryParse<CompanyType>(value, ignoreCase: true, out var companyType))
        {
            throw new ValidationException($"Invalid company type '{value}'.");
        }

        return companyType;
    }

    public async Task EnsureUniqueIdentifiersAsync(
        CompanyType companyType,
        string? taxNumber,
        string? nationalId,
        Guid? excludeCompanyId,
        CancellationToken cancellationToken)
    {
        if (companyType != CompanyType.Individual && !string.IsNullOrWhiteSpace(taxNumber))
        {
            if (await _companyRepository.ExistsByTaxNumberAsync(taxNumber, excludeCompanyId, cancellationToken))
            {
                throw new ConflictException("A company with the same tax number already exists.");
            }
        }

        if (!string.IsNullOrWhiteSpace(nationalId))
        {
            if (await _companyRepository.ExistsByNationalIdAsync(nationalId, excludeCompanyId, cancellationToken))
            {
                throw new ConflictException("A company with the same national ID already exists.");
            }
        }
    }
}
