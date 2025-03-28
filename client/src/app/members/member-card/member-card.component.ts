import { Component, input } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
//child of member list
export class MemberCardComponent {
  //use like a signal 'member().username'
  member = input.required<Member>()
}
