import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-nav',
  //formsmodule to use form
  imports: [FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  //this is the model.username, model.password in template
  model: any = {};

  //angular methods
  login() {
    console.log(this.model);
  }
}
