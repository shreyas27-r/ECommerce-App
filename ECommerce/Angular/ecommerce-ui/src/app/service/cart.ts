import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http'; 
export interface Cart {
  productId: number;
  userId: number;
  name: string;
  price: number;
  quantity: number;
}

@Injectable({
  providedIn: 'root',
})
export class CartService {

  private baseurl = 'https://localhost:7037/api/cart';

  constructor(private http: HttpClient) { }


  getCart(id: number): Observable<Cart[]> {
    return this.http.get<Cart[]>(this.baseurl + `/${id}`);
  }

  addToCart( cart: Cart): Observable<Cart> {
    return this.http.post<Cart>(this.baseurl + '/add', cart);
  }

  removeFromCart(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.baseurl}/${id}`);
  }

  checkout(userId: number): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.baseurl}/checkout/${userId}`, {});
  }

  getBill(userId: number) {
    return this.http.get<any>(`${this.baseurl}/bill/${userId}`);
  }

  updateQuantity(cartId: number, quantity: number) {
    return this.http.put(`${this.baseurl}/updateQuantity/${cartId}`, { quantity });
  }

  getOrder(userId: number) {
    return this.http.get<any>(`${this.baseurl}/order/${userId}`);
  }
}
