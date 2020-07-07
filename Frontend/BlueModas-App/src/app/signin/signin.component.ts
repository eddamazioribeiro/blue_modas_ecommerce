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
    password: new FormControl()
    // street: new FormControl(),
    // number: new FormControl(),
    // zipcode: new FormControl(),
    // city: new FormControl(),
    // state: new FormControl()
  });

  user: any = {};

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  onSubmit() {
    let user = JSON.stringify(this.userForm.value);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    this.http.post<any>(
      'http://localhost:5000/api/user', user, httpOptions).subscribe(
        data => {
          this.user = data;
          console.log(data);
        },
        err => {
          console.error(err);
        }
      );
  }

}
