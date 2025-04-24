import { AccountService } from './../_services/account.service';
import { Component, EventEmitter, inject, Input, input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  @Input() userFromHomeComponent :any;
  //userFromHomeComponent = input.required<any>(); new way of input after angular 17.3 
  @Output () cancelRegister = new EventEmitter();
  //  cancelRegister = output<boolean>(); new mwthod 

  model :any ={}
  private toastr = inject(ToastrService);

  register(){
   this.accountService.register(this.model).subscribe({
    next: response =>{
      console.log(response);
      this.cancel();
    },
    error:error => this.toastr.error(error.error)
   })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}

