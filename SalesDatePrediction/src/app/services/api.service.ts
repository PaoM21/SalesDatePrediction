import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Employees } from '../models/employees.model';
import { Products } from '../models/products.model';
import { OrderPrediction } from '../models/order-prediction.model';
import { Orders } from '../models/orders.model';
import { OrderProduct } from '../models/order-product.model';
import { Shippers } from '../models/shippers.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = `${environment.apiUrl}/employees`;

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employees[]> {
    return this.http.get<Employees[]>(this.apiUrl);
  }

  getProducts(): Observable<Products[]> {
    return this.http.get<Products[]>(`${this.apiUrl}/products`);
  }

  getCustomerOrderPredictions(): Observable<OrderPrediction[]> {
    return this.http.get<OrderPrediction[]>(`${this.apiUrl}/customers`);
  }

  getOrdersByCustomer(custId: number): Observable<Orders[]> {
    return this.http.get<Orders[]>(`${this.apiUrl}/orders/${custId}`);
  }

  createOrderWithProduct(orderProduct: OrderProduct): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/orders`, orderProduct);
  }

  getShippers(): Observable<Shippers[]> {
    return this.http.get<Shippers[]>(this.apiUrl);
  }

  getShipperById(shipperId: number): Observable<Shippers> {
    return this.http.get<Shippers>(`${this.apiUrl}/${shipperId}`);
  }
}
