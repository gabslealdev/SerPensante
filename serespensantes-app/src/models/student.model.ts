import { user } from "./user.model";
import { StudentCourse } from "./studentcourse.model";

export interface Student extends user
{
    studentsCourse: StudentCourse[]
}