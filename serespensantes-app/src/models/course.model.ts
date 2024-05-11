import { Subject } from "./subject.model";
import { Lesson } from "./lessons.model";
import { StudentCourse } from "./studentcourse.model";

export interface Course{
    id?: number,
    name: string,
    description: string,
    linkUrl: string,
    active: boolean,
    subjectId: number,
    subject: Subject,
    imagem: string,
    lessons: Lesson[],
    studentsCourse: StudentCourse[],
    duration: Date,
    createdAt: Date

}