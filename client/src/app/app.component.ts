import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  //selector is the html tag used to insert this component
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})

//oninit is like useeffect that run once after the component mounts
//lightbulb then implement oninit
export class AppComponent implements OnInit {
  //inject http so we can use it
  http = inject(HttpClient);
  //inject the accountservice where we can login and set user
  private accountService = inject(AccountService);
  title = 'Dating App';
  users: any;

  ngOnInit(): void {
    this.getUsers();
    //to check if theres user logged in
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    //this will return as object
    const user = JSON.parse(userString);
    //change global state of the user
    this.accountService.currentUser.set(user);
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
