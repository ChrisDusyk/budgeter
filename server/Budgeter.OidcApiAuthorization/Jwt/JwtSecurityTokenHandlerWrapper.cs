using Budgeter.Errors;
using Budgeter.OidcApiAuthorization.ConfigurationManager;
using Functional;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Budgeter.OidcApiAuthorization.Jwt
{
	public class JwtSecurityTokenHandlerWrapper : IJwtSecurityTokenHandlerWrapper
	{
		private readonly IOidcConnectionManager _connectionManager;

		public JwtSecurityTokenHandlerWrapper(IOidcConnectionManager connectionManager) => _connectionManager = connectionManager;

		public Result<(string Token, ClaimsPrincipal Claims), ServiceError> ValidateTokenWithRetry(string token, TokenValidationParameters tokenValidationParameters)
		{
			var handler = new JwtSecurityTokenHandler();

			try
			{
				var claimsPrinciple = handler.ValidateToken(token, tokenValidationParameters, out _);
				return Result.Success((token, claimsPrinciple));
			}
			catch (SecurityTokenSignatureKeyNotFoundException)
			{
				// A SecurityTokenSignatureKeyNotFoundException is thrown if the signing keys for
				// validating the JWT could not be found. This could happen if the issuer has
				// changed the signing keys since the last time they were retrieved by the
				// ConfigurationManager.
				_connectionManager.RequestRefresh();

				// Retry
				try
				{
					var claimsPrinciple = handler.ValidateToken(token, tokenValidationParameters, out _);
					return Result.Success((token, claimsPrinciple));
				}
				catch (Exception ex)
				{
					return Result.Failure(UnexpectedError.Create($"Unable to validate token: {ex.Message}", ex));
				}
			}
			catch (Exception ex)
			{
				return Result.Failure(UnexpectedError.Create($"Unable to validate token: {ex.Message}", ex));
			}
		}
	}
}
