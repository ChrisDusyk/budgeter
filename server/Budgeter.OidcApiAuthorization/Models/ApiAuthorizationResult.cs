using Budgeter.Errors;
using Functional;

namespace Budgeter.OidcApiAuthorization.Models
{
	public class ApiAuthorizationResult
	{
		public Option<string> UserId { get; }

		private ApiAuthorizationResult(string userId)
		{
			UserId = Option.Create(!string.IsNullOrEmpty(userId), userId);
		}

		private ApiAuthorizationResult(Option<string> userId)
		{
			UserId = userId;
		}

		public static Result<ApiAuthorizationResult, ServiceError> Create(string userId) =>
			Result.Create(!string.IsNullOrWhiteSpace(userId), () => new ApiAuthorizationResult(userId), () => ValidationError.Create($"{nameof(userId)} cannot be null", null));

		public static Result<ApiAuthorizationResult, ServiceError> Create(Option<string> userId) =>
			Result.Success(new ApiAuthorizationResult(userId));
	}
}
