import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  products: any = [];

  constructor(private location: Location) { }

  ngOnInit() {
    this.products = this.getProductsInCart();
  }

  private reload() {
    location.reload();
  }

  public getProductsInCart(): [] {
    let products = JSON.parse(localStorage.getItem('products'));

    if (products) {
      return products;
    }

    return [];
  }

  public updateCart(productId: number, quantity: number) {
    let products: any = [];
    products = this.products;

    if (quantity !== 0) {
      this.removeItem(productId);
    } else {
      let index = products.findIndex(i => i.id === productId);
      if (index !== - 1)
      {
        products[index].quantity = quantity;
      }
      localStorage.setItem('products', JSON.stringify(products));
    }
  }

  public removeItem(productId: number) {
    let products: any = [];
    products = this.getProductsInCart();

    let index = products.findIndex(i => i.id === productId);
    
    if (index !== -1) {
      products.splice(index, 1);
    }

    localStorage.setItem('products', JSON.stringify(products));

    this.reload();
  }
}
