import {TypeOperation} from "../enum/TypeOperation";
import {CategoryTypeOperation} from "../enum/CategoryTypeOperation";

export class BudgetView{
	public id: number;
	public createdDate: Date;
	public typeOperation: TypeOperation;
	public categoryTypeOperation: CategoryTypeOperation;
	public operationSum: number;
	public description: string;
	constructor(id: number,
				createdDate: Date,
				typeOperation: TypeOperation,
				categoryTypeOperation: CategoryTypeOperation,
				operationSum: number,
				description: string) {
		this.id = id;
		this.createdDate = createdDate;
		this.typeOperation = typeOperation;
		this.categoryTypeOperation = categoryTypeOperation;
		this.operationSum = operationSum;
		this.description = description;
	}
}