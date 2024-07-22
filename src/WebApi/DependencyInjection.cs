using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebApi.Common.Errors;
using WebApi.Common.Mapping;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, TaskBoardProblemDetailsFactory>();

        services.AddMappings();

        return services;
    }
}
