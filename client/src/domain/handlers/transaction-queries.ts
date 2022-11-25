import { Result } from "functional-ts-primitives";
import type { Transaction, Error } from "../models";

export const getTransactionById = (id: number): Result<Transaction, Error> => {
	return Result.success<Transaction, Error>({
		id: 0,
		amount: 10.0,
		category: {
			id: 1,
			label: "Groceries",
		},
		account: {
			id: 2,
			name: "Joint account",
			owner: "Both",
		},
		description: "Co-op",
	});
};
