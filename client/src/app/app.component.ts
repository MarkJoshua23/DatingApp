import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';
import { HomeComponent } from './home/home.component';
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  //selector is the html tag used to insert this component
  selector: 'app-root',
  imports: [RouterOutlet, NavComponent, NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})

//oninit is like useeffect that run once after the component mounts
//lightbulb then implement oninit
export class AppComponent implements OnInit {
  //inject the accountservice where we can login and set user
  private accountService = inject(AccountService);
  title = 'Dating App';

  ngOnInit(): void {
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
}
