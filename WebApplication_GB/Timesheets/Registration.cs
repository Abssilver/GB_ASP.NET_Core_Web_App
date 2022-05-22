using Microsoft.Extensions.DependencyInjection;
using Timesheets.Validation;

namespace Timesheets
{
    public static class Registration
    {
        public static IServiceCollection RegisterValidationServices(
            this IServiceCollection services)
        {
            services.AddSingleton<IDeleteClientRequestValidationService, DeleteClientRequestValidationService>();
            services.AddSingleton<IGetClientByIdRequestValidationService, GetClientByIdRequestValidationService>();
            services.AddSingleton<IGetClientByNameRequestValidationService, GetClientByNameRequestValidationService>();
            services.AddSingleton<IGetClientsWithPaginationRequestValidationService, GetClientsWithPaginationRequestValidationService>();
            services.AddSingleton<IRegisterClientRequestValidationService, RegisterClientRequestValidationService>();
            services.AddSingleton<IUpdateClientRequestValidationService, UpdateClientRequestValidationService>();
            
            services.AddSingleton<IDeleteContractRequestValidationService, DeleteContractRequestValidationService>();
            services.AddSingleton<IGetContractByIdRequestValidationService, GetContractByIdRequestValidationService>();
            services.AddSingleton<IGetContractByNameRequestValidationService, GetContractByNameRequestValidationService>();
            services.AddSingleton<IGetContractsWithPaginationRequestValidationService, GetContractsWithPaginationRequestValidationService>();
            services.AddSingleton<IRegisterContractRequestValidationService, RegisterContractRequestValidationService>();
            services.AddSingleton<IUpdateContractRequestValidationService, UpdateContractRequestValidationService>();
            
            services.AddSingleton<IDeleteEmployeeRequestValidationService, DeleteEmployeeRequestValidationService>();
            services.AddSingleton<IGetEmployeeByIdRequestValidationService, GetEmployeeByIdRequestValidationService>();
            services.AddSingleton<IGetEmployeeByNameRequestValidationService, GetEmployeeByNameRequestValidationService>();
            services.AddSingleton<IGetEmployeesWithPaginationRequestValidationService, GetEmployeesWithPaginationRequestValidationService>();
            services.AddSingleton<IRegisterEmployeeRequestValidationService, RegisterEmployeeRequestValidationService>();
            services.AddSingleton<IUpdateEmployeeRequestValidationService, UpdateEmployeeRequestValidationService>();

            return services;
        }
    }
}