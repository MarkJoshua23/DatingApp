import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  //selector is the html tag used to insert this component
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Dating App';
}
