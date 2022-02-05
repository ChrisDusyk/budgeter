import { ResultPromise } from "functional-ts-primitives";
import type { TransactionToCreate, Error, Transaction } from "../models";

export const saveNewTransactionAsync = (newTransaction: TransactionToCreate): ResultPromise<Transaction, Error> => {};
