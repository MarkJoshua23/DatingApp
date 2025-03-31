import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService)

  //start the spinner when sending a request to api
  busyService.busy()
  return next(req).pipe(
    delay(1000),
    finalize(()=>{
      //stop after finished
      busyService.idle()
    })
  );
};
