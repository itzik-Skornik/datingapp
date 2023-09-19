import { ToastrService } from 'ngx-toastr';
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
constructor(private AccountService: AccountService, private toastr : ToastrService){

}
ngOnInit(): void {
  
}
register(){
 this.AccountService.register(this.model).subscribe({
  next: () => {
    
    this.cancol();
    
  },
  error: err => {
    this.toastr.error(err.error),
    console.log(err);
    
  }
  
  
 })
  
}
cancol(){
  this.cancolRegister.emit(false);
  
}
}
