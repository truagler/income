import React, {ChangeEvent, Component} from "react";
import {Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";
import {BudgetView} from "../model/BudgetView";
import {TypeOperation} from "../enum/TypeOperation";
import {CategoryTypeOperation} from "../enum/CategoryTypeOperation";
import {Language} from "../language/Language";
import {BudgetBlank} from "../model/BudgetBlank";

export interface Props{
	isShown: boolean;
	operation: BudgetView;
	hide: () => void;
	getAllOperation: () => void;
}

type State = {
	createdDate: Date;
	typeOperation: TypeOperation;
	categoryTypeOperation: CategoryTypeOperation;
	operationSum: number;
	description: string;
}

export class BudgetModal extends Component<Props, State>{
	constructor(props: any) {
		super(props);
		
		this.state = {
			createdDate: new Date(),
			typeOperation: TypeOperation.income,
			categoryTypeOperation: CategoryTypeOperation.revenue,
			operationSum: 0,
			description: ""
		}
	}
	
	updateDate(e:ChangeEvent<HTMLDataElement>) {
		const value = e.target.value;
		const date = new Date(Date.parse(value));
		this.setState({createdDate: date})
	}
	
	updateTypeOperation(e:ChangeEvent<HTMLSelectElement>) {
		const typeOperation = Number(e.target.value);
		this.setState({typeOperation: typeOperation});
	}

	updateCategoryTypeOperation(e:ChangeEvent<HTMLSelectElement>) {
		const categoryTypeOperation = Number(e.target.value);
		this.setState({categoryTypeOperation: categoryTypeOperation});
	}

	updateOperationSum(e:ChangeEvent<HTMLInputElement>) {
		const operationSum = Number(e.target.value);
		this.setState({operationSum: operationSum});
	}

	updateDescription(e:ChangeEvent<HTMLInputElement>) {
		const description = e.target.value;
		this.setState({description: description});
	}

	async addOperation() {
		const budget = new BudgetBlank(0, this.state.createdDate, this.state.typeOperation, this.state.categoryTypeOperation, this.state.operationSum, this.state.description);
		await fetch('addoperation', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json;charset=utf-8'
			},
			body: JSON.stringify(budget)
		});
		this.props.hide();
		await this.props.getAllOperation();
	}

	async updateOperation() {
		const budget = new BudgetBlank(this.props.operation.id, this.state.createdDate, this.state.typeOperation, this.state.categoryTypeOperation, this.state.operationSum, this.state.description);
		await fetch('updateoperation', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json;charset=utf-8'
			},
			body: JSON.stringify(budget)
		});
		await this.props.getAllOperation();
	}
	
	render() {
		const typeOperation = [0, 1]
		
		return (
			<div>
				<Modal size="lg"
					   aria-labelledby="contained-modal-title-vcenter"
					   isOpen={this.props.isShown}
					   fade={false}
				>
					<ModalHeader>
						<h1>{this.props.operation.id != 0 ? "Редактирование операции: " : "Добавление операции"}</h1>
					</ModalHeader>
					<ModalBody>
						<div>
							<b>Дата: </b><input onChange={(e) => this.updateDate(e)} type="date"/>
						</div>
						<div>
							<b>Тип операции: </b>
							<select onChange={(e) => this.updateTypeOperation(e)} defaultValue={Language.toTypeOperation(this.props.operation.typeOperation)}>
								{typeOperation.map((key, value) => <option value={value} key={key}>{Language.toTypeOperation(value)}</option>)}
							</select>
						</div>
						<div>
							<b>Категория операции: </b>
							{
								this.state.typeOperation != TypeOperation.сonsumption &&

								<select onChange={(e) => this.updateCategoryTypeOperation(e)} defaultValue={Language.toCategoryTypeOperation(this.props.operation.categoryTypeOperation)}>
									 <option value={3} >{Language.toCategoryTypeOperation(3)}</option>)
									 <option value={4} >{Language.toCategoryTypeOperation(4)}</option>)
									 <option value={5} >{Language.toCategoryTypeOperation(5)}</option>)
								</select>
							}
							{
								this.state.typeOperation != TypeOperation.income &&

								<select onChange={(e) => this.updateCategoryTypeOperation(e)} defaultValue={Language.toCategoryTypeOperation(this.props.operation.categoryTypeOperation)}>
									<option value={0} >{Language.toCategoryTypeOperation(0)}</option>)
									<option value={1} >{Language.toCategoryTypeOperation(1)}</option>)
									<option value={2} >{Language.toCategoryTypeOperation(2)}</option>)
								</select>
							}
						</div>
						<div>
							<b>Сумма операции: </b><input onChange={(e) => this.updateOperationSum(e)} defaultValue={this.props.operation.operationSum} type="number"/>
						</div>
						<div>
							<b>Описание операции: </b><input onChange={(e) => this.updateDescription(e)} defaultValue={this.props.operation.description} max={20}  type="text"/>
						</div>
					</ModalBody>
					<ModalFooter>
						<button onClick={this.props.operation.id != 0 ?() => this.updateOperation() : () => this.addOperation()} className={this.props.operation.id !=0 ? "btn btn-success" : "btn btn-primary"}>{this.props.operation.id !=0 ? "Сохранить" : "Добавить"}</button>
						<button onClick={() => this.props.hide()} className="btn btn-danger">Закрыть</button>
					</ModalFooter>
				</Modal>
			</div>
		);
	}
}