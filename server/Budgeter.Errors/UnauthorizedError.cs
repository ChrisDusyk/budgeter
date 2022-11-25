using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeter.Errors
{
	public class UnauthorizedError : ServiceError
	{
		public override string ErrorType { get; } = "Unauthorized";

		private UnauthorizedError(string message, Exception innerException) : base(message, innerException) { }

		public override T Match<T>
		(
			Func<UnexpectedError, T> unexpectedError,
			Func<ValidationError, T> validationError,
			Func<UnauthorizedError, T> unauthorizedError
		) => unauthorizedError(this);

		public static ServiceError Create(string message, Exception innerException)
			=> new UnauthorizedError(message, innerException);
	}
}
