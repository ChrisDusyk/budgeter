import { Account } from "./account";
import { Category } from "./category";

export type TransactionToCreate = {
	description: string;
	category: Category;
	amount: number;
	account: Account;
};

export type Transaction = {
	id: number;
	description: string;
	category: Category;
	amount: number;
	account: Account;
};
