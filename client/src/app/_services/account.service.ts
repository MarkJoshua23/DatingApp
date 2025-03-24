import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
//SINGLETON
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //inject http
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  //it can be a User type or it can be null
  //using signal as global state management, changes here will change all that uses this
  currentUser = signal<User | null>(null);

  login(model: any) {
    //pass url and the body and return a user
    //pipe to do something with the return values, so it will executes for every component that subscribe
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      //do something to the return value of an observable
      //map just like react map so we can access user
      map((user) => {
        if (user) {
          // setItem needs the storage name and the value that will be stored
          //stringify bc localstorage can only store strings
          localStorage.setItem('user', JSON.stringify(user));
          //set the signal to user, this will rerender all components that uses this
          this.currentUser.set(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  constructor() {}
}
