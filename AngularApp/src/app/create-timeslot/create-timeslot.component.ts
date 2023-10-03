import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TimeslotsService } from '../services/timeslots.service';
import { Router } from '@angular/router';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-create-timeslot',
  templateUrl: './create-timeslot.component.html',
  styleUrls: ['./create-timeslot.component.css']
})
export class CreateTimeslotComponent implements OnInit {

  @Output() sendTimeslotList: EventEmitter<any> = new EventEmitter<any>();

  timeslotForm: FormGroup;
  timeslotlist: any[]=[];

  constructor(private fb: FormBuilder, private timeslotService: TimeslotsService, private router: Router) {
    this.timeslotForm = this.fb.group({
      date: ['', Validators.required],
      startTime: ['', [Validators.required,  this.timeFormatValidator()]],
      duration: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
      meetingLink: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  async submitForm() {
    if (this.timeslotForm.valid) {
      const id = localStorage.getItem("Id");
      const timeslotData = this.timeslotForm.value;
      timeslotData.recruiterId = id;
      await this.timeslotService.addTimeslot(timeslotData).subscribe(
        response => {
          this.timeslotlist.push(response);
          console.log('Timeslot created:', response);
        },
        error => {
          window.alert("Timeslot not available");
        }
      );
      this.router.navigate(['/recruiterdashboard']);
    } else {
      // Form is invalid, display error messages or handle accordingly
    }
  }

  sendToRecruiterDashboard() {
    // Emit the data to the parent component
    this.sendTimeslotList.emit(this.timeslotlist);
  }

timeFormatValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) {
      return null; // Allow empty input
    }

    const pattern = /^(0[1-9]|1[0-2]):[0-5][0-9] [APap][Mm]$/;
    if (!pattern.test(value)) {
      return { invalidTimeFormat: true };
    }

    // Additional validation logic if needed

    return null; // Validation passed
  };
}


}
