import { Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingsService{

  url = "https://interviewserviceteam4.azurewebsites.net/api/bookingslot"

  constructor(private httpclient : HttpClient) { }

  getBookings(): Observable<any>{
    return this.httpclient.get<any>(this.url);
  }

  addBookedslot(bookedslotData: any): Observable<any>{
    return this.httpclient.post<any>(this.url, bookedslotData);
  }

  cancleBookedslot(id: any): Observable<any>{
    const deleteUrl = `${this.url}/${id}`; 
    return this.httpclient.delete<any>(deleteUrl);
  }
}
