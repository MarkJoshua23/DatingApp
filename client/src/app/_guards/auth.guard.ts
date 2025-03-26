import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
//should return boolean
export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService)
  const toastr = inject(ToastrService)

  //if theres user logged in
  if (accountService.currentUser()){
    return true
  }else{
    toastr.error("You shall not pass!")
    return false
  }
};
