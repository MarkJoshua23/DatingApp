import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate: [authGuard],
    //children are now secured with route guard
    children: [
        { path: 'members', component: MemberListComponent },
        { path: 'members/:id', component: MemberDetailComponent },
        { path: 'lists', component: ListsComponent },
        { path: 'messages', component: MessagesComponent },
    ]
  },
  //the user need to satisfy the guard conditions to access the members page
//   { path: 'members', component: MemberListComponent, canActivate: [authGuard] },
  
  //when the route is unknown
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
