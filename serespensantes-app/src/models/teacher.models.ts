import { Lesson } from "./lessons.model";
import { user } from "./user.model";

export interface Teacher extends user
{
    lessons: Lesson[]
}