import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  registerMode = false;

  registerToggle() {
    //turn the registermode to true or false
    this.registerMode = !this.registerMode;
  }
}
