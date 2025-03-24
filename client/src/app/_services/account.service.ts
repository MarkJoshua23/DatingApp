import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
//SINGLETON
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  //inject http
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/'

  login(model: any){
    //pass url and the body
    return this.http.post(this.baseUrl + 'account/login',model);
  }
  constructor() { }
}
