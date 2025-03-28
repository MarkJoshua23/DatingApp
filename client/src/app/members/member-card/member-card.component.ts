import { Component, input } from '@angular/core';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  imports: [],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
//child of member list
export class MemberCardComponent {
  //use like a signal 'member().username'
  member = input.required<Member>()
}
