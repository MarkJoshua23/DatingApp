import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  //formsmodule to use form
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  accountService = inject(AccountService);
  //this is the model.username, model.password in template
  model: any = {};

  //angular methods
  login() {
    //the services returns an observable not the values
    //subscribe to the returned observable
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log('user', response);
      },
      error: (error) => console.log(error),
    });
  }
  logout() {
    this.accountService.logout();
  }
}
