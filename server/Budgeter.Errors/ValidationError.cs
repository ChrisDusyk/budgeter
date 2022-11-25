using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter.Errors
{
	public class ValidationError : ServiceError
	{
		public override string ErrorType { get; } = "Validation";

		private ValidationError(string message, Exception innerException) : base(message, innerException) { }

		public override T Match<T>
		(
			Func<UnexpectedError, T> unexpectedError,
			Func<ValidationError, T> validationError,
			Func<UnauthorizedError, T> unauthorizedError
		) => validationError(this);

		public static ServiceError Create(string message, Exception innerException)
			=> new ValidationError(message, innerException);
	}
}
