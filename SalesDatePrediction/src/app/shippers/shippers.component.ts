import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Shippers } from '../models/shippers.model';

@Component({
  selector: 'app-shippers',
  templateUrl: './shippers.component.html',
})
export class ShippersComponent implements OnInit {
  shippers: Shippers[] = [];
  loading: boolean = true;

  constructor(private shippersService: ApiService) { }

  ngOnInit(): void {
    this.loadShippers();
  }

  loadShippers(): void {
    this.shippersService.getShippers().subscribe(
      (data) => {
        this.shippers = data;
        this.loading = false;
      },
      (error) => {
        console.error('Error loading shippers:', error);
        this.loading = false;
      }
    );
  }
}
