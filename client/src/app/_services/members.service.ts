import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService)
  //from env
  baseUrl = environment.apiUrl;

  //this has return of Member[] type
  getMembers() {
    //header is attached by interceptor, not here
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  
  getMember(username: string) {
    //header is attached by interceptor, not here
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  //this will not be needed since header is attached by interceptor

  // getHttpOptions(){
  //   return{
  //     //headers for the request
  //     headers: new HttpHeaders({
  //       //send the token from the signal
  //       Authorization: `Bearer ${this.accountService.currentUser()?.token}`
  //     })
  //   }
  // }
  // we dont need the constructor for function version on inject
  // constructor() { }
}
