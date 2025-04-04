import { Component, inject, input, OnInit, output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, JsonPipe],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit{

  //private cannot be accessed in the template
  private accountService = inject(AccountService);
  //items that is passed from parent
  //required so it warns if theres no given parameter by the parent
  //send values from child to parent
  cancelRegister = output<boolean>();
  //to store values from form
  private toastr =inject(ToastrService)
  model: any = {};

  //needed for reactive form
  registerForm: FormGroup = new FormGroup({});


  ngOnInit(): void {
    this.initializeForm()
  }

  initializeForm(){
    //values can be seen in this.registerForm.value
    this.registerForm = new FormGroup({
      //inputs
      username: new FormControl(),
      password: new FormControl(),
      confirmPassword: new FormControl()
    })
  }

  register() {
    //to get values inside
    console.log(this.registerForm.value)
    // this.accountService.register(this.model).subscribe({
    //   next: (response) => {
    //     console.log(response);
    //     this.cancel();
    //   },
    //   error: (error) => this.toastr.error(error.error),
    // });
  }

  cancel() {
    //emit to send values to parent
    this.cancelRegister.emit(false);
  }
}
