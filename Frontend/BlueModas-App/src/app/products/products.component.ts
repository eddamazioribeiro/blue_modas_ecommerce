import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: any = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getProducts();
  }

  getProducts() {
    this.http.get(
      'http://localhost:5000/api/product').subscribe(
        res => {
          this.products = res;
        },
        err => {
          console.error(err);
        }
      );
  }

  private getProductsInCart(): [] {
    let products = JSON.parse(localStorage.getItem('products'));

    if (products) {
      return products;
    }

    return [];

  }

  public addProductToCart(product: any) {
    let products: any = [];
    products = this.getProductsInCart();

    products.push(product);
    localStorage.setItem('products', JSON.stringify(products));
  }
}
