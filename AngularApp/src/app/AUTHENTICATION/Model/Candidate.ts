export class Candidate {
    constructor(
       public userName: string,
       public password: string,
       public firstName: string,
       public lastName: string,
       public email: string,
       public phoneNumber: string,
       public role: string,
       public skills: string,
       public experience: string,
    ) {}
  }