import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseURL: string;

  constructor(private http: HttpClient) {
    this.baseURL = 'https://mercadolibro-api.app.csharpjourney.com/api/CheckOut/';
  }

  placeOrder(userId: number, checkedOutItems: Order) {
    return this.http.post<number>(this.baseURL + `${userId}`, checkedOutItems);
  }
}
