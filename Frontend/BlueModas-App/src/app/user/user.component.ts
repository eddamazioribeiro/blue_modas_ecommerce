import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  logedUser = "Área do Cliente";
  userForm = new FormGroup({
    username: new FormControl(),
    email: new FormControl(),
    phone: new FormControl(),
    password: new FormControl(),
    street: new FormControl(),
    number: new FormControl(),
    zipcode: new FormControl(),
    city: new FormControl(),
    state: new FormControl()
  });

  user: any = {};
  address: any = {};
  order: any = {};

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
  }

  onSubmit() {
    let user = {
      username: this.userForm.value.username,
      email: this.userForm.value.email,
      password: this.userForm.value.password,
      phone: this.userForm.value.phone,
    };

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    this.http.post<any>(
      'http://localhost:5000/api/user', user, httpOptions).subscribe(
        data => {
          this.user = data;
          this.saveUserAddress(data.id);
        },
        err => {
          console.error(err);
        }
      );
  }

  private saveUserAddress(userId) {
    let address = {
      street: this.userForm.value.street,
      number: parseInt(this.userForm.value.number),
      zipcode: parseInt(this.userForm.value.zipcode),
      city: this.userForm.value.city,
      state: this.userForm.value.state
    };

    const httpOptions = {
      params: userId,
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    this.http.post<any>(
      'http://localhost:5000/api/address/7', address, httpOptions).subscribe(
        data => {
          this.address = data;
          this.createOrder();
        },
        err => {
          console.error(err, address);
        }
      );
  }

  private createOrder() {
    let order = {
      userId: this.user.id,
      payment: null,
      status: 0,
      shippingAddressId: parseInt(this.address.id)
    };

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.post<any>(
      'http://localhost:5000/api/order', order, httpOptions).subscribe(
        data => {
          this.order = data;
          this.saveOrderItems(data.id);
        },
        err => {
          console.error(err, order);
        }
      );
  }

  private saveOrderItems(orderId) {
    let products: any = this.getProductsInCart();

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    products.forEach(p => {
      let item = {
        orderId: orderId,
        productId: p.id,
        price: parseFloat(p.id),
        quantity: parseInt(p.quantity)
      }
      item.orderId = orderId;

      this.http.post<any>(
        'http://localhost:5000/api/orderitem', item, httpOptions).subscribe(
          data => {
            console.log('created item', item);
          },
          err => {
            console.error('erro', item);
          }
        );
    });
  }

  public getProductsInCart(): [] {
    let products = JSON.parse(localStorage.getItem('products'));

    console.log(products);

    if (products) {
      return products;
    }

    return [];
  }
}