import { AccountService } from './_services/account.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  title = 'Dating app!!!';
  users: any;
  constructor(private http: HttpClient,private accountService:AccountService ) {


  }
  ngOnInit(): void {
    this.getUser();
    this.setCurrentUser();
  }
  getUser(){
    this.http.get("https://localhost:5001/api/user").subscribe({
      next: (res) => this.users = res,
      error: (error) => console.log(error),
      complete() {
        console.log("Request has completed");
        
      },
    })
  }
  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString)return;

const user:User = JSON.parse(userString);
this.accountService.setCurrentUser(user);
    
  }
}
