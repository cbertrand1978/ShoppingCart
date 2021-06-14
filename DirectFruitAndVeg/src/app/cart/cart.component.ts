import { Component, NgZone, OnInit } from '@angular/core';
import { CartService, CartServiceResponse } from '../cart.service';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { CartItem } from '../cart-item';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  items: CartItem[] = [];
  public temp = "Hello";
  public processingResponse: CartServiceResponse = {
    IsSuccessful: false,
    IsValid: false,
    Result: {
      CartContents: [],
      CartTotal: 0,
      SubTotal: 0,
      Discounts: []
    }
  };

  processCart(): Observable<CartServiceResponse> {
    let body = JSON.stringify(this.cartService);
    return this.http.post<CartServiceResponse>(environment.myEndpoint + '/api/shoppingcart', body, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
  
  constructor(private cartService: CartService,
              private http: HttpClient,
              private ngZone: NgZone) {
  }

  ngOnInit() {
    this.processCart().subscribe((data : any) =>
      {
        // this.processingResponse = {
        //   IsSuccessful: data.IsSuccessful,
        //   IsValid: data.IsValid,
        //   Result: data.Result
        // };
        this.processingResponse = data;
        console.log(data);
        console.log(this.processingResponse.IsSuccessful);
    });
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
}