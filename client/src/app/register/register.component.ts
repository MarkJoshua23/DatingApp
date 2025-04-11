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
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { max } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, JsonPipe, NgIf, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  //private cannot be accessed in the template
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder)
  private router = inject(Router)
  //items that is passed from parent
  //required so it warns if theres no given parameter by the parent
  //send values from child to parent
  cancelRegister = output<boolean>();
  //to store values from form

  //needed for reactive form
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date()

  validationErrors: string[]|undefined

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18)
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
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value)
    // Update just the dateOfBirth control in my form with the value stored in dob
    this.registerForm.patchValue({dateOfBirth:dob})
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => this.router.navigateByUrl('/members'),
      error: (error) => this.validationErrors= error,
    });
  }

  cancel() {
    //emit to send values to parent
    this.cancelRegister.emit(false);
  }

  getDateOnly(dob: string | undefined){
    if(!dob) return
    return new Date(dob).toISOString().slice(0,10)
  }
}
