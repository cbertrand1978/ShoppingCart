export interface Product {
  Id: number;
  Name: string;
  UnitPrice: number;
  Description: string;
}

export const products = [
  {
    Id: 1,
    Name: 'Apple',
    UnitPrice: 0.6,
    Description: 'Red Delicious'
  },
  {
    Id: 2,
    Name: 'Orange',
    UnitPrice: 0.25,
    Description: 'Clementine'
  }
];