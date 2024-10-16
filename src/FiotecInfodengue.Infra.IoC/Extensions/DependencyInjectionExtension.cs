using FiotecInfodengue.Application.Interfaces;
using FiotecInfodengue.Application.Services;
using FiotecInfodengue.Domain.Interfaces.Repositories;
using FiotecInfodengue.Domain.Interfaces.Security;
using FiotecInfodengue.Domain.Interfaces.Services;
using FiotecInfodengue.Domain.Services;
using FiotecInfodengue.Infra.Secutiry.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FiotecInfodengue.Infra.IoC.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();

        #region Application Layer

        services.AddTransient<IUsuarioAppService, UsuarioAppService>();

        #endregion

        #region Domain Layer

        services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
        services.AddTransient<IAuthorizationSecurity, AuthorizationSecurity>();

        #endregion

        #region InfraStructure Layer

        services.AddTransient<IUsuarioRepository, UsuarioRepository>();

        #endregion

        return services;
    }
}