using Logistic.ApplicationService.Features.Customers.Handlers;
using Logistic.ApplicationService.Features.Customers.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Logistic.ApplicationService.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CustomerValidationService>();

        services.AddScoped<CreateCustomerCommandHandler>();
        services.AddScoped<UpdateCustomerCommandHandler>();
        services.AddScoped<DeactivateCustomerCommandHandler>();
        services.AddScoped<AddCustomerContactCommandHandler>();
        services.AddScoped<AddCustomerNoteCommandHandler>();
        services.AddScoped<AddCustomerBankAccountCommandHandler>();
        services.AddScoped<GetCustomerByIdQueryHandler>();
        services.AddScoped<GetCustomerListQueryHandler>();

        return services;
    }
}
