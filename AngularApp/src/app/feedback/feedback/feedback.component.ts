import { Component } from '@angular/core';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent {
  selectedRating: number = 0;
  feedbackText: string = '';

  submitFeedback() {
    // Implement your logic to submit the feedback here
    console.log('Rating: ' + this.selectedRating);
    console.log('Feedback Text: ' + this.feedbackText);

    // Clear the form fields if needed
    this.selectedRating = 0;
    this.feedbackText = '';
  }
}