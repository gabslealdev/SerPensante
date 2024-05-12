import { Component, HostListener } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSignInAlt } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'srp-header',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  faSignInAlt = faSignInAlt;

  constructor() {
    
  }

  @HostListener('window:scroll')
  onScroll() {
    const header = document.querySelector("#header")
    if(header != undefined){
      if(window.scrollY > header.scrollTop)
        {
          header?.classList.add("scrollTop")
        }
        else
        {
          header?.classList.remove("scrollTop")
        }
    }
    
  }
}
