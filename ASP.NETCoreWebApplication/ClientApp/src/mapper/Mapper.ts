import {BudgetView} from "../model/BudgetView";

export class Mapper{
	static toBudgetViewModel(data: any): BudgetView{
		return new BudgetView(data.id, data.createdDate, data.typeOperation, data.categoryTypeOperation, data.operationSum, data.description);
	}

	static toBudgetViewModels(data: any[]): BudgetView[]{
		return data.map(book=> this.toBudgetViewModel(book));
	}
}