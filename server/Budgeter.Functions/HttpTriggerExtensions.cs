using Budgeter.Errors;
using Budgeter.Functions.Models;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Budgeter.Functions
{
	internal static class HttpTriggerExtensions
	{
		public static async Task<HttpResponseData> CreateSuccessResponse<T>(this HttpRequestData req, HttpStatusCode statusCode, T responseBodyData)
		{
			var response = req.CreateResponse(statusCode);
			await response.WriteAsJsonAsync(responseBodyData);
			return response;
		}

		public static Task<HttpResponseData> CreateErrorResponse(this HttpRequestData req, ServiceError serviceError) =>
			serviceError.Match(
				async unexpectedError =>
				{
					var response = req.CreateResponse(HttpStatusCode.InternalServerError);
					response.Headers.Add("Content-Type", "application/json");
					await response.WriteStringAsync(JsonSerializer.Serialize(unexpectedError.ToErrorResponse()));
					return response;
				},
				async validationError =>
				{
					var response = req.CreateResponse(HttpStatusCode.BadRequest);
					response.Headers.Add("Content-Type", "application/json");
					await response.WriteStringAsync(JsonSerializer.Serialize(validationError.ToErrorResponse()));
					return response;
				},
				async unauthorizedError =>
				{
					var response = req.CreateResponse(HttpStatusCode.Unauthorized);
					response.Headers.Add("Content-Type", "application/json");
					await response.WriteStringAsync(JsonSerializer.Serialize(unauthorizedError.ToErrorResponse()));
					return response;
				});
	}
}
