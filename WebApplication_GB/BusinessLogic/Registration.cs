using BusinessLogic.Abstractions.Services;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Registration
    {
        public static IServiceCollection RegisterBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IContractService, ContractService>();
            return services.AddTransient<IPersonService, PersonService>();
        }
    }
}