import { Component } from '@angular/core';
import { CartService } from '../../service/cart';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './cart-component.html',
  styleUrl: './cart-component.css',
})
export class CartComponent
{
  cartItems: any[] = [];
  userId = 0;
  orders: any[] = [];
  ordersLoaded = false;
  showBill = false;
  deliveryFee = 50;
  totalAmount = 0;
  bill: any = null;

  ngOnInit() {
    if (typeof window !== 'undefined') {
      this.userId = Number(localStorage.getItem("userId"));
    }
  }
  constructor(private cartService: CartService) { }

  getCart() {

    console.log("Fetching cart for user:", this.userId);

    this.cartService.getCart(this.userId).subscribe({
      next: (data) => {
        console.log(data);
        this.cartItems = data;

      },
      error: (err) => {
        console.log(err);

      }
    });
  }

  removeFromCart(id: number)
  {
    this.cartService.removeFromCart(id).subscribe({
      next: (data) => {
        console.log(data);
        alert(data.message);   

        this.getCart();
      },
      error: (err) => {
        console.log(err);
      }
    });

  }

  checkout(userId: number)
  {
    this.cartService.checkout(userId).subscribe({
      next: (data) => {
        console.log(data);
        alert(data.message);   
        this.getCart();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  openBill() {
    this.cartService.getBill(this.userId).subscribe({
      next: (data) => {
        this.bill = data;
        this.showBill = true;
      }
    });
  }

  addQuantity(item: any) {
    console.log("Before +", item.quantity);
    item.quantity++;
    console.log("After +", item.quantity);
    this.cartService.updateQuantity(item.id, item.quantity).subscribe({
      next: () => {
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  subtractQuantity(item: any) {
    if (item.quantity > 1) {
      item.quantity--;

      this.cartService.updateQuantity(item.id, item.quantity).subscribe({
        next: () => {
          this.getCart();
        },
        error: (err) => {
          console.log(err);
        }
      });
    }
  }


  getOrder(userId: number) {
    this.cartService.getOrder(userId).subscribe({
      next: (data) => {
        this.orders = data;
        this.ordersLoaded = true;

      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}

