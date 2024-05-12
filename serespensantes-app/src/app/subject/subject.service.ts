import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Subject } from "../../models/subject.model";
import { environment } from "../../environments/environment";
import { map } from "rxjs";



@Injectable({
    providedIn: 'root'
})
export class SubjectService{

    private url = environment.api
    
    constructor(private http: HttpClient) {}

    getSubjects() 
    {
        return this.http.get<Subject[]>(`${this.url}/v1/subjects`)
        .pipe(map((response: any) => {return response.data}))
    }
}