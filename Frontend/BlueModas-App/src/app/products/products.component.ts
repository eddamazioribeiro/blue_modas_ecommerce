import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: any = [];
  imagemAltura = 200;
  imagemLargura = 200;
  imagemMargem = 2;

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

    let index = products.findIndex(i => i.id === product.id);
    
    if (index == -1) {
      product.quantity = 1;
    } else {
      product.quantity = products[index].quantity += 1;
      products.splice(index, 1);
    }

    products.push(product);
    console.log(products);

    localStorage.setItem('products', JSON.stringify(products));
  }
}
