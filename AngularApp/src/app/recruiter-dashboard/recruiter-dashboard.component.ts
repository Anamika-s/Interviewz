import { Component, OnInit } from '@angular/core';
import { TimeslotsService } from '../services/timeslots.service';
import { Timeslot } from '../models/timeslot';
import { Router } from '@angular/router';
import { RegistrationService } from '../AUTHENTICATION/Service/registration.service';

@Component({
  selector: 'app-recruiter-dashboard',
  templateUrl: './recruiter-dashboard.component.html',
  styleUrls: ['./recruiter-dashboard.component.css']
})
export class RecruiterDashboardComponent implements OnInit {

  timeslotList: any[] = [];
  timeslotlist: Timeslot[] = [];
  id: any;
  meetingDate: any;
  meetingStartTime: any;
  meetingDuration: any;
  meetingLink: any;

  showCreateTimeslot = false;

  recruiterName: any;
  recruiterId: any;
  length : any =0;

  constructor(private timeslotSerice: TimeslotsService, private registrationService: RegistrationService, private router: Router) { }

  ngOnInit(): void {
    this.getAllTimeslots();
    this.getRecruiterDetail(localStorage.getItem('Email'));
    // this.getRecruiterDetail("string");

  }

  async getRecruiterDetail(email: any): Promise<any> {
    try{
      const recruiters = await this.registrationService.getAllRecruiter().toPromise();
      console.log('Recruites', recruiters);
      for(let recruiter of recruiters){
        if(recruiter.email == email){
          this.recruiterName = recruiter.firstName;
          this.recruiterId = recruiter.id;
          console.log('Recruiter Name', this.recruiterName);
          console.log('Recruiter Id', this.recruiterId);
          break;
        }
      }
    }
    catch (error) {
      console.error('Error fetching candidates', error);
    }
  }

  async getAllTimeslots(): Promise<void>{
    this.timeslotlist = [];
    await this.timeslotSerice.getTimeslots().subscribe(response => {
      this.timeslotList = response;
      // Example: Assuming each timeslot object has a 'name' and 'time' property
      for (const timeslotObject of this.timeslotList) {
        // Check if 'name' and 'time' properties exist before accessing them
        
        if('id' in timeslotObject){
          this.id = timeslotObject['id'];
        }
        if ('date' in timeslotObject){
          this.meetingDate = timeslotObject['date'];
        }
        if ('startTime' in timeslotObject){
          this.meetingStartTime = timeslotObject['startTime'];
        }
        if ('duration' in timeslotObject){
          this.meetingDuration = timeslotObject['duration'];
        }
        if ('meetingLink' in timeslotObject){
          this.meetingLink = timeslotObject['meetingLink'];
          const recruiterID = localStorage.getItem("Id");
          if(recruiterID == timeslotObject['recruiterId']){
            this.timeslotlist.push(new Timeslot(this.id,this.meetingDate, this.meetingStartTime, this.meetingDuration, this.meetingLink, false));
            length += 1;
          }
        }
         else {
          console.log('Invalid timeslot object structure');
        }
      }

      console.log('Timeslots', response);
    },
      error => {
        console.error('Error creating timeslot:', error);
      }
    );
  }

  async deleteTimeslot(id: string){
    await this.timeslotSerice.deleteTimeslot(id).subscribe(res=>{
      console.log("Delete Timeslot");
      this.length =0;
      this.getAllTimeslots();
    })
  }

  createTimeslot() {
    // Add your logic here to create a new timeslot
    this.router.navigate(['/create-timeslot']);
  }

  receiveDataFromCreateTimeslot(data: any) {
    this.timeslotlist.push(data);
    length += 1;
    // this.timeslots = data;
    console.log("Received Data frorm create Table: "+data);
  }
}
