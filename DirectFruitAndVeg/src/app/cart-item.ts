import { Product } from './products';

export interface CartItem {
  Quantity: number;
  TotalPrice: number;
  Product: Product;
}