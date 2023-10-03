import { Component, OnInit } from '@angular/core';
import { BookingsService } from '../services/bookings.service';
import { TimeslotsService } from '../services/timeslots.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bookslot',
  templateUrl: './bookslot.component.html',
  styleUrls: ['./bookslot.component.css'],
})
export class BookslotComponent implements OnInit {


  bookingsWithTimeSlots: any[] = [];
  length: any = 0;
  isLoading: boolean = true;

  constructor(private bookingService: BookingsService, private timeslotService: TimeslotsService, private router : Router) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.length = 0;
    this.getBookedSlotsWithTimeSlots();
  }

  async getAllBookedslots(): Promise<void> {
    await this.bookingService.getBookings().subscribe(data => {
      console.log("Bookings: ", data);
    });
  }

  async getBookedSlotsWithTimeSlots(): Promise<void> {
    try {
      const bookedSlots = await this.bookingService.getBookings().toPromise();
      console.log("BookSlot", bookedSlots);
      for (const bookedSlot of bookedSlots) {
        try {
          const timeSlotId = bookedSlot.timeSlotID;
          if (timeSlotId) {
            const timeSlot = await this.timeslotService.getTimeSlotById(timeSlotId).toPromise();
            const role = localStorage.getItem("Role");
            if (role == "Recruiter") {
              const RecruiterId = localStorage.getItem("Id");
              const recruiterId = timeSlot.recruiterId;
              if (RecruiterId == recruiterId) {
                this.bookingsWithTimeSlots[bookedSlot.bookingID] = timeSlot;
                this.length += 1;
              }
            }
            else {
              this.bookingsWithTimeSlots[bookedSlot.bookingID] = timeSlot;
              this.length += 1;
            }
          }
          else {
            console.error('Invalid timeSlotId: ', timeSlotId);
          }
        } catch (error) {
          console.error(`Error fetching time slot for BookingID ${bookedSlot.BookingID}:`, error);
        }
      }
    } catch (error) {
      console.error('Error fetching booked slots:', error);
    }
    finally {
      this.isLoading = false;
    }
  }

  async cancelBooking(bookingId: any) {
    try {
      // Check if the bookingId exists in this.bookingsWithTimeSlots
      if (this.bookingsWithTimeSlots.hasOwnProperty(bookingId)) {
        const booking = this.bookingsWithTimeSlots[bookingId];
        if (booking) {
          const timeslotId = booking.id;
          // Use timeslotId as needed
          await this.updateTimeslot(timeslotId);
        } else {
          console.log(`No entry found for bookingId: ${bookingId}`);
        }
      } else {
        console.log(`No entry found for bookingId: ${bookingId}`);
        return; // Return early if bookingId is not found
      }
      await this.bookingService.cancleBookedslot(bookingId).toPromise();
      console.log("Canceled Bookedslot");
      // Refresh the bookings with time slots
      delete(this.bookingsWithTimeSlots[bookingId]);
      this.length -= 1;
    } catch (error) {
      console.error('Error cancelling booking:', error);
    }
  }


  async updateTimeslot(id: string): Promise<void> {
    try {
      let isBooked = false;
      const response = await this.timeslotService.updateTimeslot(id, isBooked).toPromise();
      console.log('Timeslot updated:', response);
    } catch (error) {
      console.error('Error updating timeslot:', error);
    }
  }
  

  openMeeting(meetingLink: string) {
  // Implement the logic to open the meeting link here, e.g., navigate to the link
  window.open(meetingLink, '_blank');
}

routeToTimeslot(){
  const role = localStorage.getItem("Role");
  if(role == "Recruiter"){
    this.router.navigate(["/recruiterdashboard"])
  }
  else{
    this.router.navigate(["/candidatedashboard"])
  }
}
}
