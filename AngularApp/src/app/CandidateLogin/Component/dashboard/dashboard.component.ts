import { Component, OnInit } from '@angular/core';
import { RegistrationService } from 'src/app/AUTHENTICATION/Service/registration.service';
import { TimeslotsService } from 'src/app/services/timeslots.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  recruiters: any[]=[];
  candidateId: any;
  candidateName: any;
  constructor(private registrationService: RegistrationService, private router : Router) { }
  ngOnInit(): void {
    this.getAllRecruiters();
    this.getCandidateDetail(localStorage.getItem('Email'));
  }

  async getAllRecruiters() {
    await this.registrationService.getAllRecruiter().subscribe(data => {
      this.recruiters = data;
      console.log("Recruiters: ", data);
    });
  }

  async getCandidateDetail(email: any): Promise<any>{
    try{
      const candidates = await this.registrationService.getAllCandidate().toPromise();
      console.log('Candidate', candidates);
      for(let candidate of candidates){
        if(candidate.email == email){
          this.candidateName = candidate.firstName;
          this.candidateId = candidate.candidateId;
          console.log('Candidate Name', this.candidateName);
          console.log('Candidate Id', this.candidateId);
        }
      }
    }
    catch (error) {
      console.error('Error fetching candidates', error);
    }
  }

  showAvailability(recruiterId:any){
    localStorage.setItem("recruiterId", recruiterId);
    this.router.navigate(['/timeslot']);
  }
  bookInterview(recruiterId: number): void {
    
  }
}


