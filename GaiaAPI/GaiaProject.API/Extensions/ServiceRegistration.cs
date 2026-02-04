using GaiaProject.Application.Interfaces;
using GaiaProject.Application.Operations;
using GaiaProject.Application.Options;
using GaiaProject.Application.Services;
using GaiaProject.Domain.Interfaces;
using GaiaProject.Infrastructure.Repositories;
using System.Reflection;

namespace GaiaProject.API.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Repositories
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IOperationHistoryRepository, OperationHistoryRepository>();

            // Register Services
            services.AddScoped<IOperationService, OperationService>();

            // Register Configuretions
            services.Configure<OperationHistoryOptions>(
                configuration.GetSection("OperationHistory"));

            // Register Operation Executors
            services.AddScoped<IOperationExecutor, AdditionOperationExecutor>();
            services.AddScoped<IOperationExecutor, SubtractionOperationExecutor>();
            services.AddScoped<IOperationExecutor, MultiplicationOperationExecutor>();
            services.AddScoped<IOperationExecutor, DivisionOperationExecutor>();
            services.AddScoped<IOperationExecutor, ModuloOperationExecutor>();
            services.AddScoped<IOperationExecutor, PowerOperationExecutor>();
            services.AddScoped<IOperationExecutor, ConcatenationOperationExecutor>();
            services.AddScoped<IOperationExecutor, CompareOperationExecutor>();
            services.AddScoped<IOperationExecutor, ContainsOperationExecutor>();
            services.AddScoped<IOperationExecutor, LengthCompareOperationExecutor>();

            return services;
        }
    }
}
