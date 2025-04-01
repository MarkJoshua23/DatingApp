import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  //from env
  baseUrl = environment.apiUrl;
  //type Member array with initial value of empty array
  members = signal<Member[]>([]);

  //this has return of Member[] type
  getMembers() {
    //header is attached by interceptor, not here
    return this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
      //store the fetched data to members signal as a singleton
      next: (members) => this.members.set(members),
    });
  }

  getMember(username: string) {
    //get the member info from members where the username is the passed username
    const member = this.members().find((x) => x.username == username);

    //return member if it has values and not undefined
    // 'of' makes it observable
    if (member !== undefined) return of(member);

    //header is attached by interceptor, not here
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      //also updates the members signal
      tap(() => {
        console.log(member)
        this.members.update((members) =>
          members.map((m) => (
            
            m.username === member.username ? member : m))
        );
      })
    );
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
