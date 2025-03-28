import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-detail',
  //for tabs bootstrap
  imports: [TabsModule,GalleryModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
})
export class MemberDetailComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);

  //type of gallery item isfrom ng-gallery
  images: GalleryItem[]=[]

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
      next: (member) => {
        this.member = member
        //photos is the list of photos of the user
        member.photos.map(p=> {
          //push each photos to the images array
          this.images.push(new ImageItem({src: p.url, thumb:p.url}))
        })
      },
    });
  }
}
