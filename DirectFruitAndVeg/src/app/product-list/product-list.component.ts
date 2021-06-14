import { Component } from '@angular/core';
import { CartItem } from '../cart-item';
import { CartService } from '../cart.service';
import { Product, products } from '../products';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent {
  products = products;
  cartItems = this.cartService.getItems();
  
  constructor(private cartService: CartService) {}

  addToCart(product: Product) {
    var cartItem = {
      Quantity: 1,
      TotalPrice: 0,
      Product: product
    };

    this.cartService.addToCart(cartItem);
  }

  removeFromCart(product: Product) {
    var cartItem = {
      Quantity: 1,
      TotalPrice: 0,
      Product: product
    };

    this.cartService.removeFromCart(cartItem);
  }
}