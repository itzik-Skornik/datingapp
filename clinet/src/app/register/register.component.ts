import { AccountService } from './../_services/account.service';
import { Component, Input, Output, EventEmitter ,OnInit} from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
@Output() cancolRegister = new EventEmitter();
model :any={};
constructor(private AccountService: AccountService){

}
ngOnInit(): void {
  
}
register(){
 this.AccountService.register(this.model).subscribe({
  next: () => {
    
    this.cancol();
    
  },
  error: err => console.log(err)
  
  
 })
  
}
cancol(){
  this.cancolRegister.emit(false);
  
}
}
