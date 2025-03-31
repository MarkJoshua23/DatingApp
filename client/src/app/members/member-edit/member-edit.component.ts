import { Component, HostListener, inject, OnInit, ViewChild, ɵgetInjectableDef } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  imports: [TabsModule, FormsModule,],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  //to access the ngform to see if its dirty
  @ViewChild('editForm') editForm?:NgForm;
  //beforeunload is the event when leaving the page using the browser
  //browser will show dialog
  @HostListener('window:beforeunload',['$event']) notify($event:any){
    if (this.editForm?.dirty){
      $event.returnValue = true
    }
  }
  member?: Member;
  private accountService = inject(AccountService)
  private memberService = inject (MembersService )
  private toastr = inject(ToastrService)
  ngOnInit(): void {
   this.loadMember()
  }

  loadMember(){
    const user = this.accountService.currentUser()
    if (!user) return
    //feth information about the currentuser
    this.memberService.getMember(user.username).subscribe({
      
      next: member => this.member = member
    })
  }

  updateMember(){
    console.log(this.editForm?.value)
    //to send just the needed fields and not all member values
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success("Profile updated successfully!")
        //after submitted reset it to member
        this.editForm?.reset(this.member)
      }
    })

  }
}
