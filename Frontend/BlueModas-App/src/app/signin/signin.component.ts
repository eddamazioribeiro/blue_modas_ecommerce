import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {

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

  constructor(private http: HttpClient) { }

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
      "http://localhost:5000/api/address/7", address, httpOptions).subscribe(
        data => {
          console.log(data);
        },
        err => {
          console.error(err, address);
        }
      );
  }
}
