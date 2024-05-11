import { Course } from "./course.model"

export interface Subject
{ 
    id?: number,
    name: string,
    science: string
    courses?: Course[]
}