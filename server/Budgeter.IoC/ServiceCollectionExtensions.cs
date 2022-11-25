using Budgeter.OidcApiAuthorization;
using Budgeter.OidcApiAuthorization.AuthorizationHeader;
using Budgeter.OidcApiAuthorization.ConfigurationManager;
using Budgeter.OidcApiAuthorization.Jwt;
using Budgeter.OidcApiAuthorization.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgeter.IoC
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterDomain(this IServiceCollection services)
			=> services;

		public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<OidcApiAuthorizationSettings>(configuration.GetSection(nameof(OidcApiAuthorizationSettings)));

			return services
				.AddSingleton<IJwtSecurityTokenHandlerWrapper, JwtSecurityTokenHandlerWrapper>()
				.AddSingleton<IOidcConnectionManager, OidcConnectionManager>()
				.AddSingleton<IAuthorizationHeaderBearerTokenExtractor, AuthorizationHeaderBearerTokenExtractor>()
				.AddSingleton<IApiAuthorization, OidcAuthorizationService>();
		}
	}
}
