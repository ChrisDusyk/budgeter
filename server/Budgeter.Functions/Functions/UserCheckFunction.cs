using Budgeter.Errors;
using Budgeter.OidcApiAuthorization;
using Budgeter.OidcApiAuthorization.Models;
using Functional;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Budgeter.Functions.Functions
{
	public class UserCheckFunction
	{
		private readonly IApiAuthorization _apiAuthorization;

		public UserCheckFunction(IApiAuthorization apiAuthorization)
		{
			_apiAuthorization = apiAuthorization;
		}

		[Function(nameof(UserCheckFunction))]
		public Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
		{
			var logger = executionContext.GetLogger(nameof(UserCheckFunction));
			logger.LogInformation("C# HTTP trigger function processed a request.");

			var result =
				from authResult in _apiAuthorization.AuthorizeAsync(req.Headers)
				from handlerResult in Handler(authResult, logger)
				select handlerResult;

			return result.MatchAsync(
				s => req.CreateSuccessResponse(HttpStatusCode.OK, s),
				f => req.CreateErrorResponse(f));
		}

		private Task<Result<ResponseData, ServiceError>> Handler(ApiAuthorizationResult apiAuthorizationResult, ILogger logger)
		{
			var userId = apiAuthorizationResult.UserId.Match(s => s, () => "service token");
			logger.LogInformation($"Handler running for user: {userId}");
			return Task.FromResult(Result.Success<ResponseData, ServiceError>(new ResponseData() { UserId = userId, Timestamp = DateTime.UtcNow }));
		}
	}

	internal struct ResponseData
	{
		public string UserId { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
