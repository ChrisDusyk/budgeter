using Budgeter.OidcApiAuthorization.Models;
using Functional;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgeter.OidcApiAuthorization.AuthorizationHeader;
using Budgeter.OidcApiAuthorization.ConfigurationManager;
using Budgeter.OidcApiAuthorization.Jwt;
using Microsoft.Extensions.Options;
using Budgeter.Errors;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker.Http;

namespace Budgeter.OidcApiAuthorization
{
	public class OidcAuthorizationService : IApiAuthorization
	{
		private readonly IAuthorizationHeaderBearerTokenExtractor _authorizationHeaderBearerTokenExtractor;
		private readonly IJwtSecurityTokenHandlerWrapper _jwtSecurityTokenHandlerWrapper;
		private readonly IOidcConnectionManager _connectionManager;

		private readonly Option<string> _issuerUrl = Option.None();
		private readonly Option<string> _audience = Option.None();

		public OidcAuthorizationService(
			IAuthorizationHeaderBearerTokenExtractor authorizationHeaderBearerTokenExtractor,
			IJwtSecurityTokenHandlerWrapper jwtSecurityTokenHandlerWrapper,
			IOidcConnectionManager connectionManager,
			IOptions<OidcApiAuthorizationSettings> options)
		{
			_authorizationHeaderBearerTokenExtractor = authorizationHeaderBearerTokenExtractor ?? throw new ArgumentNullException(nameof(authorizationHeaderBearerTokenExtractor));
			_jwtSecurityTokenHandlerWrapper = jwtSecurityTokenHandlerWrapper ?? throw new ArgumentNullException(nameof(jwtSecurityTokenHandlerWrapper));
			_connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));

			_issuerUrl = Option.FromNullable(options.Value?.IssuerUrl);
			_audience = Option.FromNullable(options.Value?.Audience);
		}

		public Task<Result<ApiAuthorizationResult, ServiceError>> AuthorizeAsync(HttpHeadersCollection httpRequestHeaders)
		{
			// OTel stuff eventually

			return
				from authTokenOption in _authorizationHeaderBearerTokenExtractor.GetTokenFromHeaders(httpRequestHeaders)
				from authToken in authTokenOption.ToResult(() => UnauthorizedError.Create("No auth token provided in Authorization header", null))
				from userId in ValidateTokenAndExtractUserId(authToken)
				from finalApiAuthorizationResult in ApiAuthorizationResult.Create(userId)
				select finalApiAuthorizationResult;
		}

		private Task<Result<Option<string>, ServiceError>> ValidateTokenAndExtractUserId(string authToken) =>
				from signingKeys in _connectionManager.GetIssuerSigningKeysAsync()
				from tokenValidationParams in GetTokenValidationParameters(signingKeys)
				from tokenAndClaims in _jwtSecurityTokenHandlerWrapper.ValidateTokenWithRetry(authToken, tokenValidationParams)
				from userId in GetUserIdFromClaims(tokenAndClaims.Claims)
				select userId;

		private Result<TokenValidationParameters, ServiceError> GetTokenValidationParameters(IEnumerable<SecurityKey> issuerSecurityKeys) =>
			from audience in _audience.ToResult(() => UnexpectedError.Create("Audience is missing from configuration", null))
			from issuerUrl in _issuerUrl.ToResult(() => UnexpectedError.Create("Issuer URL is missing from configuration", null))
			select new TokenValidationParameters()
			{
				RequireSignedTokens = true,
				ValidAudience = audience,
				ValidateAudience = true,
				ValidIssuer = issuerUrl,
				ValidateIssuerSigningKey = true,
				ValidateLifetime = true,
				IssuerSigningKeys = issuerSecurityKeys
			};

		private static Result<Option<string>, ServiceError> GetUserIdFromClaims(ClaimsPrincipal claimsPrincipal) =>
			Result.Try(
				() => Option.FromNullable(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value),
				ex => UnexpectedError.Create(ex.Message, ex));
	}
}
