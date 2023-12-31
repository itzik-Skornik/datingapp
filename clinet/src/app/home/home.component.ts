import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMode = false;
users:any;
constructor(private http: HttpClient){

}
ngOnInit(): void {
  this.getUser();
}
registerToggle(){
  this.registerMode = !this.registerMode
}
cancolRegisterModel(event : boolean){
  this.registerMode = event;
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
}
