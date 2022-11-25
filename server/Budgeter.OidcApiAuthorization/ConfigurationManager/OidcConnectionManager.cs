using Budgeter.Errors;
using Budgeter.OidcApiAuthorization.Models;
using Functional;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Budgeter.OidcApiAuthorization.ConfigurationManager
{
	public class OidcConnectionManager : IOidcConnectionManager
	{
		private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

		public OidcConnectionManager(IOptions<OidcApiAuthorizationSettings> options)
		{
			var issuerUrl = options.Value.IssuerUrl;
			var documentRetriever = new HttpDocumentRetriever()
			{
				RequireHttps = issuerUrl.StartsWith("https")
			};

			_configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
				$"{issuerUrl}.well-known/openid-configuration",
				new OpenIdConnectConfigurationRetriever(),
				documentRetriever);
		}

		/// <summary>
		/// Returns the cached signing keys if they were retrieved previously.
		/// If they haven't been retrieved, or the cached keys are stale, then a fresh set of
		/// signing keys are retrieved from the OpenID Connect provider (issuer) cached and returned.
		/// This method will throw if the configuration cannot be retrieved, instead of returning null.
		/// </summary>
		/// <returns>
		/// The current set of the Open ID Connect issuer's signing keys.
		/// </returns>
		public Task<Result<ICollection<SecurityKey>, ServiceError>> GetIssuerSigningKeysAsync() =>
			Result.TryAsync(async () =>
			{
				var config = await _configurationManager.GetConfigurationAsync(CancellationToken.None);
				return config.SigningKeys;
			})
			.MapOnFailure(ex => UnexpectedError.Create(ex.Message, ex));

		/// <summary>
		/// Requests that the next call to GetIssuerSigningKeysAsync() obtain new signing keys.
		/// If the last refresh was greater than RefreshInterval then the next call to
		/// GetIssuerSigningKeysAsync() will retrieve new configuration (signing keys).
		/// If RefreshInterval == System.TimeSpan.MaxValue then this method does nothing.
		/// </summary>
		/// <remarks>
		/// RefreshInterval defaults to 30 seconds (00:00:30).
		/// </remarks>
		public Result<Unit, ServiceError> RequestRefresh() =>
			Result.Try(() => _configurationManager.RequestRefresh())
			.MapOnFailure(ex => UnexpectedError.Create(ex.Message, ex));
	}
}
