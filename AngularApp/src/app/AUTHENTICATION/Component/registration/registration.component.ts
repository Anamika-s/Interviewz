import { Component} from '@angular/core';
import { RegistrationService } from '../../Service/registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent  {

  BG = "assets/images/Bg.jpg";
  candidate: any = {}; 
  recruiter: any = {}; 

  constructor(private registrationService: RegistrationService, private router : Router) {}
  

  registerCandidate() { 
    console.log("In Com " + JSON.stringify(this.candidate));
    
    this.registrationService.registerCandidate(this.candidate).subscribe(
      (response) => {
        console.log('Candidate registered:', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        window.alert("Email already registered")
      }
    );
  }

  registerRecruiter() { 
    this.registrationService.registerRecruiter(this.recruiter).subscribe(
      (response) => {
        console.log('Recruiter registered:', response);
        this.router.navigate(['/login']);
      },
      (error) => {
        window.alert("Email already registered");        
      }
    );
  }
}