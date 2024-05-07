import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSquareRootAlt } from '@fortawesome/free-solid-svg-icons';
import { faAtlas } from '@fortawesome/free-solid-svg-icons';
import { faMicroscope } from '@fortawesome/free-solid-svg-icons';
import { faCode } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'srp-subject',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.css'
})
export class SubjectComponent {
  Exacts = faSquareRootAlt
  Humans = faAtlas
  Biologicals = faMicroscope
  Tech = faCode
  
  public activeList: boolean = true
  public exactsCourses: string[] = ["Física - I", "Física - II", "Física - III", "Matemática Aplicada", "Geometria Analítica"]
  public humansCourses: string[] = ["Historia - I", "Historia - II", "Historia - III", "Revisão de historia, fundamental", "Revisão de geografica, fundamental"] 
  public biologicalsCourses: string[] = ["Biologia - I", "Biologia - II", "Biologia - III"]
  public techCourses: string[] = ["Lógica de programação", "Linguagem de Programação - Python", "Linguagem de Programação - C#", "Linguagem de Programação - JavaScript"]
}

