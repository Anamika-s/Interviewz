import { Component, OnInit } from '@angular/core';
import { Timeslot } from '../models/timeslot';
import { TimeslotsService } from '../services/timeslots.service';
import { Router } from '@angular/router';
import { BookingsService } from '../services/bookings.service';
import { BookedSlots } from '../models/bookedslots';
import { RegistrationService } from '../AUTHENTICATION/Service/registration.service';

@Component({
  selector: 'app-timeslot',
  templateUrl: './timeslot.component.html',
  styleUrls: ['./timeslot.component.css']
})
export class TimeslotComponent implements OnInit {

  timeslotList: any[] = [];
  timeslots: Timeslot[] = [];
  id: any;
  meetingDate: any;
  meetingStartTime: any;
  meetingDuration: any;
  meetingLink: any;
  meetingBooked: any;

  constructor(private timeslotService: TimeslotsService, private bookingsService: BookingsService, private registrationService: RegistrationService,private router: Router) { }

  ngOnInit(): void {
    this.getAllTimeslots();
  }

  getAllTimeslots(): void{
    this.timeslots = [];
    this.timeslotService.getTimeslots().subscribe(response => {
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
        }
        if('booked' in timeslotObject){
          this.meetingBooked = timeslotObject['booked'];
          const RecruiterId = localStorage.getItem("recruiterId");
          const recruiterId = timeslotObject['recruiterId'];
          if(this.meetingBooked == false && RecruiterId==recruiterId) 
          this.timeslots.push(new Timeslot(this.id,this.meetingDate, this.meetingStartTime, this.meetingDuration, this.meetingLink, this.meetingBooked));
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

  async booktimeslot(id: any): Promise<void>{
    await this.updateTimeslot(id);
    await this.addBookingSlot(id);
    this.router.navigate(['/bookslot']);
  }


  async updateTimeslot(id: any): Promise<void> {
    try {
      let isBooked = true;
      const response = await this.timeslotService.updateTimeslot(id, isBooked).toPromise();
      console.log('Timeslot updated:', response);
      this.getAllTimeslots();
    } catch (error) {
      console.error('Error updating timeslot:', error);
    }
  }
  

  async addBookingSlot(id: any): Promise<void>{
    try {
      const candidateId = localStorage.getItem('Id');
      try{
        const timeslot = await this.timeslotService.getTimeSlotById(id).toPromise();
        console.log("Timeslot by Id: ", timeslot);
        if(timeslot){
          const recruiterId = timeslot.recruiterId;
          const meetingLink = timeslot.meetingLink;
          const recruiter = await this.registrationService.getRecruiterById(recruiterId).toPromise();
          const candidateEmail = localStorage.getItem('Email');
          const nBookedlot = new BookedSlots(null, recruiter.email, meetingLink, candidateEmail, id);
          const data = await this.bookingsService.addBookedslot(nBookedlot).toPromise();
          console.log(data);
          console.log("Slot is booked", id);
        }
        else{
          const nBookedlot = new BookedSlots(null, null, null, candidateId, id);
          const data = await this.bookingsService.addBookedslot(nBookedlot).toPromise();
          console.log(data);
          console.log("Slot is booked", id);
        }
        
      }
      catch(error){
        console.error("Error occurred while geting timeslot by id while booking", error);
      }
    } catch (error) {
      console.error("Error occurred while booking slot:", error);
    }
  }
}


