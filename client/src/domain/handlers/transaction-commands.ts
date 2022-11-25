import { Result, ResultPromise } from "functional-ts-primitives";
import type { TransactionToCreate, Error, Transaction } from "../models";

export const saveNewTransactionAsync = (newTransaction: TransactionToCreate): ResultPromise<Transaction, Error> => {
	return Result.successAsync<Transaction, Error>(() =>
		Promise.resolve({
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
		})
	);
};
