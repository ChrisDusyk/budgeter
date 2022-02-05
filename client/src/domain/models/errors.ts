// Error Types
export type UnexpectedError = {
	message: string;
	details: string;
	errorCode: ErrorCode;
};

export type ValidationError = {
	message: string;
	property: string;
};

export type Error = UnexpectedError | ValidationError;

// Error Codes
export type BaseErrorCode = {
	code: "BaseError";
};

export type ErrorCode = BaseErrorCode;
