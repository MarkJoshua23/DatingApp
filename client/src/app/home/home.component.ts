import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  //inject http so we can use it
  http = inject(HttpClient);
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle() {
    //turn the registermode to true or false
    this.registerMode = !this.registerMode;
  }
  cancelRegisterMode(event: boolean) {
    //to cancel the register form ui
    this.registerMode = event;
  }
  getUsers() {
    //use 'this.' when using class property
    //all observables need subscribe
    this.http.get('https://localhost:5001/api/users').subscribe({
      //what will happen next after theres a return
      //put the respose to the class property 'users'
      next: (response) => (this.users = response),

      //what will happen if theres error
      error: (error) => {
        console.log(error);
      },

      //what will happen after next
      complete: () => console.log('Request has completed' + this.users),
    });
  }
}
