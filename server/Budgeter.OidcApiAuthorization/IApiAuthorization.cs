using Budgeter.Errors;
using Budgeter.OidcApiAuthorization.Models;
using Functional;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;

namespace Budgeter.OidcApiAuthorization
{
	public interface IApiAuthorization
	{
		Task<Result<ApiAuthorizationResult, ServiceError>> AuthorizeAsync(HttpHeadersCollection httpRequestHeaders);

		//Task<HealthCheckResult> HealthCheckAsync();
	}
}
