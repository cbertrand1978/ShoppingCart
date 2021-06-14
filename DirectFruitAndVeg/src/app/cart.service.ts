import { Injectable } from '@angular/core';
import { CartItem } from './cart-item';
import { Discount } from './discount'

export interface ShoppingCart {
  CartContents: CartItem[],
  CartTotal: number,
  SubTotal: number,
  Discounts: Discount[]
}

export interface CartServiceResponse{
  IsValid: boolean,
  IsSuccessful: boolean,
  Result: ShoppingCart
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  CartContents: CartItem[] = [];

  constructor() {}

  addToCart(cartItem: CartItem) {
    var item = this.CartContents.find(x => x.Product.Id === cartItem.Product.Id);

    if (item == null)
    {
      this.CartContents.push(cartItem);
    }
    else
    {
      var index = this.CartContents.indexOf(item);
      this.CartContents[index].Quantity += 1;
    }
  }

  removeFromCart(cartItem: CartItem) {
    var item = this.CartContents.find(x => x.Product.Id === cartItem.Product.Id);

    if (item != null)
    {
      var index = this.CartContents.indexOf(item);

      if (this.CartContents[index].Quantity - 1 <= 0)
      {
        var index = this.CartContents.indexOf(item);
        this.CartContents.splice(index, 1);
      }
      else
      {
        this.CartContents[index].Quantity -= 1;
      }
    }
  }

  getItems() {
    return this.CartContents;
  }

  clearCart() {
    this.CartContents = [];
    return this.CartContents;
  }
}