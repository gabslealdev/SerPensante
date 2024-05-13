
import { Routes } from "@angular/router"
import { SearchComponent } from "./search/search.component"
import { SubjectComponent } from "./subject/subject.component"
import { CoursesComponent } from "./courses/courses.component"

export const ROUTES: Routes = [
    {path: '', component: SearchComponent},
    {path: '', component: SubjectComponent},
    {path: '/subject/science:string', component: CoursesComponent}
]
    