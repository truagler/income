import React, {ChangeEvent, Component, useState} from "react";
import {BudgetView} from "../model/BudgetView";
import {TypeOperation} from "../enum/TypeOperation";
import {CategoryTypeOperation} from "../enum/CategoryTypeOperation";
import {Mapper} from "../mapper/Mapper";
import {Language} from "../language/Language";
import {BudgetModal} from "../modal/BudgetModal";
import Accordion from 'react-bootstrap/Accordion'

export interface Props{}

type State = {
  operations: BudgetView[];
  operation: BudgetView;
  revenue: number;
  isShowModal: boolean;
  ids: number[];
  startDate: Date;
  stopDate: Date;
  month: Date;
  year: Date;
}

export class FetchData extends Component<Props, State> {
  static displayName = FetchData.name;

  constructor(props: any) {
    super(props);
    this.state = { 
      operations: [],
      operation:{
        id: 0,
        createdDate: new Date(),
        typeOperation: TypeOperation.income,
        categoryTypeOperation: CategoryTypeOperation.car,
        operationSum: 0,
        description: ""
      },
      revenue: 0,
      isShowModal: false,
      ids:[],
      startDate: new Date(),
      stopDate: new Date(),
      month: new Date(),
      year: new Date()
    };
  }

  componentDidMount() {
    this.getAllOperations();
  }
  
  async getOperation(id: number) {
    const response = await fetch('getoperation?id=' + id.toString());
    const data = await response.json();
    const operation = Mapper.toBudgetViewModel(data);
    this.setState({operation: operation});
    this.openEditModal();
  }
  
  async getWeekOperation(startDate: Date, stopDate: Date) {
    const response = await fetch('getweekoperations?startDate=' + startDate.toDateString() + '&stopDate=' + stopDate.toDateString());
    const data = await response.json();
    const operations = Mapper.toBudgetViewModels(data);
    this.setState({operations: operations});
  }
  
  async getMonthOperation(month: Date) {
    const response = await fetch('getmonthoperations?month=' + month.toDateString());
    const data = await response.json();
    const operations = Mapper.toBudgetViewModels(data);
    this.setState({operations: operations});
  }
  
  async getYearOperation(year: Date) {
    const response = await fetch('getyearoperations?year=' + year.toDateString());
    const data = await response.json();
    const operations = Mapper.toBudgetViewModels(data);
    this.setState({operations: operations});
  }
  
  async getAllOperations() {
    const response = await fetch('getoperations');
    const data = await response.json();
    const operations = Mapper.toBudgetViewModels(data);
    this.setState({operations: operations});
    await this.getFullRevenue();
  }
  
  async getFullRevenue() {
    const response = await fetch('getfullrevenue');
    const data = await response.json();
    const revenue = Number(data);
    this.setState({revenue: revenue});
  }
  
  async removeOperation(id: number) {
    await fetch('removeoperation', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      },
      body: JSON.stringify(id)
    });
    await this.getAllOperations();
    await this.getFullRevenue();
  }

  async removeOperations() {
    if(this.state.ids.length == 0) return alert("Вы не выбрали ни одной операции");
    await fetch('removeoperations', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      },
      body: JSON.stringify(this.state.ids)
    });
    await this.getFullRevenue();
  }

  updateStartDate(e:ChangeEvent<HTMLInputElement>){
    const value = e.target.value;
    const date = new Date(Date.parse(value));
    this.setState({startDate: date})
  }

  updateStopDate(e:ChangeEvent<HTMLInputElement>){
    const value = e.target.value;
    const date = new Date(Date.parse(value));
    this.setState({stopDate: date})
  }

  updateMonthDate(e:ChangeEvent<HTMLInputElement>){
    const value = e.target.value;
    const date = new Date(Date.parse(value));
    this.setState({month: date})
  }

  updateYearDate(e:ChangeEvent<HTMLInputElement>){
    const value = e.target.value;
    const date = new Date(Date.parse(value));
    this.setState({year: date})
  }
  
  updateIds(e:ChangeEvent<HTMLInputElement>) {
    const checked = e.target.checked;
    const id = Number(e.target.value);
    if(!this.state.ids.includes(id) && checked){
      this.state.ids.push(id);
    }
    if(this.state.ids.includes(id) && !checked){
      let index = this.state.ids.indexOf(id);
      this.state.ids.splice(index, 1)
    }
  }
  
  openEditModal = () =>{
    this.setState({  isShowModal: true});
  }
  openAddModal = () =>{
    this.setState({  isShowModal: true, operation:{id: 0, createdDate: new Date(), typeOperation: TypeOperation.income, categoryTypeOperation: CategoryTypeOperation.car, operationSum: 0, description: ""}});
  }

  closeModal = () =>{
    this.setState({  isShowModal: false});
  }
  
  render() {

    return (
      <div>
        <h1>Ваши доходы/расходы</h1>
        <div>
          <b>Деньги на вашем счете: {this.state.revenue} Pублей</b>
        </div>
        <hr/>
        <div>
          <button onClick={() => this.openAddModal()} className="btn btn-primary">Добавить</button>
          <button onClick={() => this.removeOperations()} className="btn btn-danger">Удалить несколько</button>
          <button onClick={() => this.getAllOperations()} className="btn btn-success">Получить все операции</button>
        </div>
        <Accordion>
          <Accordion.Item eventKey="0">
            <Accordion.Header>Получние операций за период</Accordion.Header>
            <Accordion.Body>
              <div>
                <b>Начало недели: </b><input onChange={(e) => this.updateStartDate(e)} type="date"/>
              </div>
              <div>
                <b>Конец недели: </b><input onChange={(e) => this.updateStopDate(e)} type="date"/>
              </div>
              <div>
                <button onClick={() => this.getWeekOperation(this.state.startDate, this.state.stopDate )} className="btn btn-primary">Получить данные</button>
              </div>
            </Accordion.Body>
          </Accordion.Item>
          <Accordion.Item eventKey="1">
            <Accordion.Header>Получение операций за месяц</Accordion.Header>
            <Accordion.Body>
              <div>
                <b>Введите дату: </b><input onChange={(e) => this.updateMonthDate(e)} type="date"/>
              </div>
              <div>
                <button onClick={() => this.getMonthOperation(this.state.month)} className="btn btn-primary">Получить данные</button>
              </div>
            </Accordion.Body>
          </Accordion.Item>
          <Accordion.Item eventKey="2">
            <Accordion.Header>Получение операций за год</Accordion.Header>
            <Accordion.Body>
              <div>
                <b>Введите дату: </b><input onChange={(e) => this.updateYearDate(e)} type="date"/>
              </div>
              <div>
                <button onClick={() => this.getYearOperation(this.state.year)} className="btn btn-primary">Получить данные</button>
              </div>
            </Accordion.Body>
          </Accordion.Item>
        </Accordion>
        <table className='table table-striped'>
          <thead>
          <tr>
            <th>Дата операции</th>
            <th>Тип операции</th>
            <th>Категория</th>
            <th>Сумма операции</th>
            <th>Описание</th>
            <th></th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          {this.state.operations.map(operation =>
              <tr key={operation.id}>
                <td>{operation.createdDate.toString().slice(0,10)}</td>
                <td onClick={() => this.getOperation(operation.id)}>{Language.toTypeOperation(operation.typeOperation)}</td>
                <td>{Language.toCategoryTypeOperation(operation.categoryTypeOperation)}</td>
                <td>{operation.operationSum} Р</td>
                <td>{operation.description}</td>
                <td><button className="btn btn-danger" onClick={() => this.removeOperation(operation.id)}>Удалить</button></td>
                <td><input onChange={(e) => this.updateIds(e)} value={operation.id} type="checkbox"/></td>
              </tr>
          )}
          </tbody>
        </table>
        <BudgetModal getAllOperation={() => this.getAllOperations()} hide={() => this.closeModal()} isShown={this.state.isShowModal} operation={this.state.operation}/>
      </div>
    );
  }
}
