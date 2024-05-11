import { Role } from "./Enum/role.enum"

export interface user
{
    id?: number,
    name: string,
    birhDate: Date,
    contact: string,
    email: string,
    active: boolean,
    passwordHash: string,
    image: string
    role: Role
}