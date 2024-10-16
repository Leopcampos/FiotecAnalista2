using FiotecInfodengue.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace FiotecInfodengue.Infra.IoC.Extensions;

public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoToEntityMap));
        return services;
    }
}