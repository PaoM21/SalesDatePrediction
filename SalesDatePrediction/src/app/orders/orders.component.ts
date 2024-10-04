import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Orders } from '../models/orders.model';
import { OrderProduct } from '../models/order-product.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders: Orders[] = [];
  newOrder: OrderProduct = {
    empId: 0,
    shipperId: 0,
    shipName: '',
    shipAddress: '',
    shipCity: '',
    orderDate: new Date(),
    requiredDate: new Date(),
    shippedDate: null,
    freight: 0,
    shipCountry: '',
    productId: 0,
    unitPrice: 0,
    qty: 0,
    discount: 0
  };

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadOrdersByCustomer(1);
  }

  loadOrdersByCustomer(custId: number): void {
    this.apiService.getOrdersByCustomer(custId).subscribe(
      (data) => {
        this.orders = data;
      },
      (error) => {
        console.error('Error fetching orders', error);
      }
    );
  }

  createOrder(): void {
    this.apiService.createOrderWithProduct(this.newOrder).subscribe(
      (response) => {
        console.log(response);
        this.loadOrdersByCustomer(1);
      },
      (error) => {
        console.error('Error creating order', error);
      }
    );
  }
}
