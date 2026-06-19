import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'; 
import { User } from '../Component/user/user';
export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  category: number;
}

@Injectable({
  providedIn: 'root',
})
export class ProductService
{
  private baseurl = 'https://localhost:7037/api/product';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseurl + '/products');
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.baseurl + '/add', product);
  }

  deleteProduct(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.baseurl}/${id}`);
  }

  editProduct(product: Product,id: number): Observable<Product> {
    return this.http.put<Product>(`${this.baseurl}/${id}`, product);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseurl+ '/users');
  }
}
