import { Component, inject, input, OnInit, output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgIf } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  //private cannot be accessed in the template
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder)
  //items that is passed from parent
  //required so it warns if theres no given parameter by the parent
  //send values from child to parent
  cancelRegister = output<boolean>();
  //to store values from form
  private toastr = inject(ToastrService);
  model: any = {};

  //needed for reactive form
  registerForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    //values can be seen in this.registerForm.value
    this.registerForm = this.fb.group({
      //inputs
      gender: ['male'],
      username: ['', Validators.required],
      knownAs:['', Validators.required],
      dateOfBirth:['', Validators.required],
      city:['', Validators.required],
      country:['', Validators.required],
      password: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(8),
      ]],
      confirmPassword: ['', [
        Validators.required,
        this.matchValues('password'),
      ]],
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next:()=> this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { isMatching: true };
    };
  }

  register() {
    //to get values inside
    console.log(this.registerForm.value);
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
