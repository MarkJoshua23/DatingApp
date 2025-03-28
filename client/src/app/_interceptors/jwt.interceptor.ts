import { HttpInterceptorFn } from '@angular/common/http';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
//intercept requests before sending to api
export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService)

  //if theres current user logged in
  if (accountService.currentUser()){
    //make a clone of req and put headers to it
    req = req.clone({
      setHeaders:{
        Authorization: `Bearer ${accountService.currentUser()?.token}`
      }
    })
  }
  //now we made modification to cloned req and send it to the server
  //now all requests of the app will have token header attached
  return next(req);
};
