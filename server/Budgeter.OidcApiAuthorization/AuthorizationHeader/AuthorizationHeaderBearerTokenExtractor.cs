using Budgeter.Errors;
using Functional;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Budgeter.OidcApiAuthorization.AuthorizationHeader
{
	public class AuthorizationHeaderBearerTokenExtractor : IAuthorizationHeaderBearerTokenExtractor
	{
		public Result<Option<string>, ServiceError> GetTokenFromHeaders(HttpHeadersCollection httpRequestHeaders) =>
			Result.Try(() =>
			{
				if (httpRequestHeaders.TryGetValues("Authorization", out var rawAuthorizationHeaderValue))
				{
					if (!AuthenticationHeaderValue.TryParse(rawAuthorizationHeaderValue.First(), out var authenticationHeaderValue))
						return Option.None<string>();

					if (!string.Equals(authenticationHeaderValue.Scheme, "Bearer", StringComparison.InvariantCultureIgnoreCase))
						return Option.None<string>();

					return Option.Some(authenticationHeaderValue.Parameter);
				}
				else
				{
					return Option.None<string>();
				}
				
			})
			.MapOnFailure(ex => UnexpectedError.Create(ex.Message, ex));
	}
}
