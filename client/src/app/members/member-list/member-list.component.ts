import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
//oninit is like useeffect

export class MemberListComponent implements OnInit{
  memberService = inject(MembersService)
  //runs when the component loaded
  ngOnInit(): void {
    //only fetch/load if theres no content inside
    if(this.memberService.members().length===0) this.loadMembers()


  }

  loadMembers(){
    this.memberService.getMembers()
  }

}
