using System;

namespace Budgeter.Errors
{
	public class UnexpectedError : ServiceError
	{
		public override string ErrorType { get; } = "Unexpected";

		private UnexpectedError(string message, Exception innerException) : base(message, innerException) { }

		public override T Match<T>
		(
			Func<UnexpectedError, T> unexpectedError,
			Func<ValidationError, T> validationError,
			Func<UnauthorizedError, T> unauthorizedError
		) => unexpectedError(this);

		public static ServiceError Create(string message, Exception innerException)
			=> new UnexpectedError(message, innerException);
	}
}
