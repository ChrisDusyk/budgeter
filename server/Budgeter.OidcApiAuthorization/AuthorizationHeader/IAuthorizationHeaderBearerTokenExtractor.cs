using Budgeter.Errors;
using Functional;
using Microsoft.Azure.Functions.Worker.Http;

namespace Budgeter.OidcApiAuthorization.AuthorizationHeader
{
	public interface IAuthorizationHeaderBearerTokenExtractor
	{
		Result<Option<string>, ServiceError> GetTokenFromHeaders(HttpHeadersCollection httpRequestHeaders);
	}
}
