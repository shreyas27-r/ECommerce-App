import { Component } from '@angular/core';
import { ProductService } from '../../service/product';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CartService } from '../../service/cart';
import { RouterLink } from '@angular/router';
import { User } from '../user/user';
import { UserService } from '../../service/user';

@Component({
  selector: 'app-product-component',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './product-component.html',
  styleUrl: './product-component.css',
})
export class ProductComponent
{
  product = {
    id: 0,
    name: '',
    description: '',
    imageUrl: '',
    price: 0,
    category: 0
  };

  imageBaseUrl = 'https://localhost:7037';

  showaddForm = false;
  showEditForm = false;

  products: any[] = [];
  users: any[] = [];

  isAdmin = false;
  userName = '';
  
constructor(
  private productService: ProductService,
  private cartService: CartService,
  private userService: UserService
  ) { }

  ngOnInit() {

    this.isAdmin = localStorage.getItem("role") === "admin";
    this.userName= localStorage.getItem("userName") || '';
  }

  addProduct() {
    console.log("Product:", this.product);

    this.productService.addProduct(this.product).subscribe({
      next: () => {
        alert("Product Added Successfully");
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getProducts() {
    this.productService.getProducts().subscribe({
      next: (data) => {
        console.log(data);
        this.products = data;
      }
    });
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: (data) => {
        console.log(data);
        this.users = data;
      }
    });
  }

  deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe({
      next: (data) => {
        console.log(data);
        alert(data.message);   // data is the returned string

        this.getProducts();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  editProduct(product: any, id: number) {
    console.log(product);

    this.productService.editProduct(product,id).subscribe({
      next: (data) => {
        console.log(data);
        alert("Product Updated Successfully");
        this.getProducts();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  addToCart(product: any) {

    const cart = {
      userId: Number(localStorage.getItem("userId")),
      productId: product.id,
      name: '',
      price: 0,
      quantity: 1
    };

    console.log(cart);

    this.cartService.addToCart(cart).subscribe({
      next: (data) => {
        alert("Product Added to Cart Successfully");
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  openAddForm() {
    this.showaddForm = true;
  }

  closeAddForm() {
    this.showaddForm = false;
  }

  openEditForm(product: any) {
    this.showEditForm = true;

    this.product = {
      id: product.id,
      name: product.name,
      description: product.description,
      imageUrl: product.imageUrl,
      price: product.price,
      category: product.category
    };
  }

  closeEditForm() {
    this.showEditForm = false;
  }

}
