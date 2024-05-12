import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { SearchComponent } from './search/search.component';
import { SubjectComponent } from './subject/subject.component';

@Component({
  selector: 'srp-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, SearchComponent, SubjectComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'serespensantes-app';
}
