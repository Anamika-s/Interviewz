import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


const headers = new HttpHeaders({
   
    "Access-Control-Allow-Origin": "*",
    "Content-Type": "application/json",
    "Accept": "application/json"   
});

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private apiUrl = 'https://localhost:7259';

  //private candidateUrl = 'https://authserviceteam4.azurewebsites.net/api/candidates';
  private candidateUrl = 'https://interqapp.azure-api.net/interview-app/api/candidates';
   // private recruiterUrl = 'https://authserviceteam4.azurewebsites.net/api/recruiters';
  private recruiterUrl = 'https://interqapp.azure-api.net/interview-app/api/recruiters';
  constructor(private http: HttpClient) { }

  registerCandidate(candidateData: any) { 
    console.log(this.candidateUrl);
    return this.http.post(`${this.candidateUrl}`, candidateData,
     { headers: headers }); 
  }

  registerRecruiter(recruiterData: any) {
    console.log('Recruiter Data:', recruiterData)
    return this.http.post(`${this.recruiterUrl}`, recruiterData, { headers: headers });
  }

  getAllRecruiter(): Observable<any>{
    return this.http.get<any>(`${this.recruiterUrl}`);
  }

  getAllCandidate(): Observable<any>{
    return this.http.get<any>(`${this.candidateUrl}`);
  }

  getCandidateById(id: any): Observable<any>{
    return this.http.get<any>(`${this.candidateUrl}/${id}`);
  }

  getRecruiterById(id: any): Observable<any>{
    return this.http.get<any>(`${this.recruiterUrl}/${id}`);
  }
}
