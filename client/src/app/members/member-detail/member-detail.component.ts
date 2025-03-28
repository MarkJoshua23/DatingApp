import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-detail',
  imports: [],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
})
export class MemberDetailComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);

  member?: Member;

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    //username is the members/:username route
    const username = this.route.snapshot.paramMap.get('username');
    //do nothing and return if theres no username
    if (!username) return;

    this.memberService.getMember(username).subscribe({
      //assign the return of http request to the class props 'member'
      next: (member) => (this.member = member),
    });
  }
}
