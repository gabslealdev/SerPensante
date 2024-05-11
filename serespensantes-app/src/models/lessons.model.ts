import { Course } from "./course.model";
import { Teacher } from "./teacher.models";

export interface Lesson{
    id?: number,
    title: string,
    duration: Date,
    linkUrl: string,
    watched: boolean,
    teacherId: number,
    teacher: Teacher,
    courseId: number,
    course: Course
}