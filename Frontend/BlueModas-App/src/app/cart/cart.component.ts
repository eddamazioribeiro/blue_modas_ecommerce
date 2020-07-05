import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  products: any = [];

  constructor() { }

  ngOnInit() {
    this.getProductsInCart();
  }

  public getProductsInCart() {
    this.products = JSON.parse(localStorage.getItem('products'));
  }

  public updateCart() {
    // code here
  }
}
