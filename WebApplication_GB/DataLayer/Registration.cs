using DataLayer.Abstractions.Repositories;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            return services.AddTransient<IContractRepository, ContractRepository>();
        }
    }
}