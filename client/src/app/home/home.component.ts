import { Component } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
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
