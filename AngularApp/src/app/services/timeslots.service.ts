import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';



const Headers = new HttpHeaders({
  'Content-Type': 'application/json',
});
@Injectable({
  providedIn: 'root'
})
export class TimeslotsService {

  url = "https://interqapp.azure-api.net/interviewapp/api/TimeSlot/"
  constructor(private httpClient: HttpClient) { }

  getTimeslots(): Observable<any>{
    return this.httpClient.get<any>(this.url);
  }

  getTimeSlotById(timeSlotId: string): Observable<any> {
    const apiUrl = `${this.url}/${timeSlotId}`; // Construct the full URL
    return this.httpClient.get<any>(apiUrl);
  }

  addTimeslot(timeslotData: any): Observable<any>{
    return this.httpClient.post<any>(this.url, timeslotData);
  }

  deleteTimeslot(id: any): Observable<any>{
    const deleteUrl = `${this.url}/${id}`;
    return this.httpClient.delete<any>(deleteUrl);
  }

  updateTimeslot(id: any, isBooked: any): Observable<any>{
    const url = `${this.url}/${id}?isBooked=${isBooked}`;
    return this.httpClient.put<any>(url, null);
  }
}
