import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',
  //formsmodule to use form
  //BsDropdownModule for dropdown directives
  //routerlink for routers
  //routerlinkactive to specify the active link/button/tab so we can style it accordingly
  //titlecasepipe to manipulate case
  imports: [FormsModule, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  //use this service to use login or access the currentuser state
  accountService = inject(AccountService);
  private toastr= inject(ToastrService)
  //this is the model.username, model.password in template
  model: any = {};

  //switch routes programatically
  private router = inject(Router);

  //angular methods
  login() {
    //the services returns an observable not the values
    //subscribe to the returned observable
    this.accountService.login(this.model).subscribe({
      next: () => {
        //navigate the user to members component
        this.router.navigateByUrl('/members');
      },
      error: (error) => this.toastr.error(error.error),
    });
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
