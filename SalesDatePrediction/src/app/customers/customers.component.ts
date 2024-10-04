import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { OrderPrediction } from '../models/order-prediction.model';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  orderPredictions: OrderPrediction[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadCustomerOrderPredictions();
  }

  loadCustomerOrderPredictions(): void {
    this.apiService.getCustomerOrderPredictions().subscribe(
      (data) => {
        this.orderPredictions = data;
      },
      (error) => {
        console.error('Error fetching customer order predictions', error);
      }
    );
  }
}
