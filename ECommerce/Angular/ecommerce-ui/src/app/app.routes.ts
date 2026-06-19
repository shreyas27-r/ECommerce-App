import { Routes } from '@angular/router';
import { HomeComponent } from './Component/home-component/home-component';
import { ProductComponent } from './Component/product-component/product-component';
import { CartComponent } from './Component/cart-component/cart-component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'products',
    component: ProductComponent
  },
  {
    path: 'cart',
    component: CartComponent
  }

];
