import { Component, Input, OnInit, inject } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faSquareRootAlt } from '@fortawesome/free-solid-svg-icons';
import { faAtlas } from '@fortawesome/free-solid-svg-icons';
import { faMicroscope } from '@fortawesome/free-solid-svg-icons';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { Subject } from '../../models/subject.model';
import { SubjectService } from './subject.service';
import {HttpClientModule } from '@angular/common/http';




@Component({
  selector: 'srp-subject',
  standalone: true,
  imports: [FontAwesomeModule, HttpClientModule],
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.css'
})
export class SubjectComponent implements OnInit {
  public Exacts = faSquareRootAlt
  public Humans = faAtlas
  public Biologicals = faMicroscope
  public Tech = faCode
  
  public subjects: Subject[] = []

  constructor(private service: SubjectService) { }

  ngOnInit(): void {
     this.service.getSubjects().subscribe(subject => this.subjects = subject)
  }

}

