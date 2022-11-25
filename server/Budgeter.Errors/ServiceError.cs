using System;

namespace Budgeter.Errors
{
	public abstract class ServiceError : Exception
	{
		public abstract string ErrorType { get; }

		protected ServiceError(string message, Exception innerException) : base(message, innerException)
		{ }

		public abstract T Match<T>
		(
			Func<UnexpectedError, T> unexpectedError,
			Func<ValidationError, T> validationError,
			Func<UnauthorizedError, T> unauthorizedError
		);
	}
}
