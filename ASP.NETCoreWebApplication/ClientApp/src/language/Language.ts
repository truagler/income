import {TypeOperation} from "../enum/TypeOperation";
import {CategoryTypeOperation} from "../enum/CategoryTypeOperation";

export class Language{
	static toTypeOperation(type: TypeOperation){
		switch (type){
			case TypeOperation.income: return "Доход"
				break;
			case TypeOperation.сonsumption: return "Расход"
				break;
		}
	}
	
	static toCategoryTypeOperation(type: CategoryTypeOperation){
		switch (type){
			case CategoryTypeOperation.funny: 
				return "Развлечения"
			
			case CategoryTypeOperation.food: 
				return "Еда"
			
			case CategoryTypeOperation.car: 
				return "Транспорт"
			
			case CategoryTypeOperation.revenue: 
				return "Зарплата"
			
			case CategoryTypeOperation.refundTax: 
				return "Возврат налогов"
			
			case CategoryTypeOperation.present: 
				return "Подарок"
		}
	}
}