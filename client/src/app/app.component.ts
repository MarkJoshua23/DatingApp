import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";

@Component({
  //selector is the html tag used to insert this component
  selector: 'app-root',
  imports: [RouterOutlet,NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})

//oninit is like useeffect that run once after the component mounts
//lightbulb then implement oninit
export class AppComponent implements OnInit {
  //inject http so we can use it
  http = inject(HttpClient);
  title = 'Dating App';
  users: any;

  ngOnInit(): void {
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
