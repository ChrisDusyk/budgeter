using Budgeter.Errors;
using Functional;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Budgeter.OidcApiAuthorization.Jwt
{
	public interface IJwtSecurityTokenHandlerWrapper
	{
		Result<(string Token, ClaimsPrincipal Claims), ServiceError> ValidateTokenWithRetry(string token, TokenValidationParameters tokenValidationParameters);
	}
}
