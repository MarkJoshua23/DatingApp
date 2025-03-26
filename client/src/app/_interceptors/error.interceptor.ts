import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  //observable so we can do something with the return using pipe
  return next(req).pipe(
    //catch errors or requests interceted and do something with it
    catchError((error) => {
      if (error) {
        //base it on the statuscode
        switch (error.status) {
          case 400:
            if (error.error.errors) {
              const modalStateErrors = [];
              for (const key in error.error.errors) {
                if (error.error.errors[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                  // toastr.error(error.error.errors[key], error.status)
                }
              }
              //makes it to one array and throw it as error, like console,log
              throw modalStateErrors.flat();
            } else {
              //if its just a error with string response
              toastr.error(error.error, error.status);
            }
            break;

          case 401:
            toastr.error('Unauthorized', error.status);
            break;

          //route to notfound page
          case 404:
            router.navigateByUrl('not-found');
            break;

          case 500:
            //this will carry the errors
            const navigationExtras: NavigationExtras = {
              state: { error: error.error },
            };
            //navigate to error page with the error values
            router.navigateByUrl('/server-error', navigationExtras);
            break;
          default:
            toastr.error('Something unexpected went wrong');
            break;
        }
      }
      throw error;
    })
  );
};
