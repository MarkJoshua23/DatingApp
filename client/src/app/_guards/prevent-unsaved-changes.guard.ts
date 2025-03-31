import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

//candeactivate to make conditional if user really want to leave the page
//put the component to attach guard in <>
//we only need 'component' as parameter
export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  //component can access anything inside the component in <MemberEditComponent>
  if(component.editForm?.dirty){
    //confirm returns a true if confirmed and false if canceled
    //true means it go to the page, false it stays in this page
    return confirm('Unsaved changes will be lost once you leave this page')
  }
  return true;
};
