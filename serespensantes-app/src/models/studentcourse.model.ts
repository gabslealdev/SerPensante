import { Course } from "./course.model";
import { Student } from "./student.model";

export interface StudentCourse{
    studentId: number
    student: Student
    courseId: number
    course: Course
    progress: number
    starDate: Date
    endDate: Date

}