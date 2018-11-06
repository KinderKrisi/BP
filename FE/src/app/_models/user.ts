import { HospitalProfile } from "./hospitalProfile";

export class User {
    email: string;
    password: string;
    id: number;
    role: string;
    profiles: HospitalProfile[];
}