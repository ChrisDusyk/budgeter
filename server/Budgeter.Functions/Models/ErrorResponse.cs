using Budgeter.Errors;

namespace Budgeter.Functions.Models
{
	internal class ErrorResponse
	{
		public string Message { get; set; }
		public string ErrorType { get; set; }
		public string StackTrace { get; set; }
	}

	internal static class ErrorResponseExtensions
	{
		public static ErrorResponse ToErrorResponse(this ServiceError serviceError) =>
			new ErrorResponse()
			{
				Message = serviceError.Message,
				ErrorType = serviceError.ErrorType,
				StackTrace = serviceError.StackTrace
			};
	}
}
