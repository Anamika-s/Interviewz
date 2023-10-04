import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  // private candidateUrl = 'https://localhost:7259/login/candidate';
  // private recruiterUrl = 'https://localhost:7259/login/recruiter';

  private candidateUrl = 'https://interqapp.azure-api.net/interview-app/api/Authentication/candidate/login';
  // private candidateUrl = 'https://authserviceteam4.azurewebsites.net/api/authentication/candidate/login';
  // private recruiterUrl = 'https://authserviceteam4.azurewebsites.net/api/authentication/recruiter/login';
  private recruiterUrl = 'https://interqapp.azure-api.net/interview-app/api/Authentication/recruiter/login';

  constructor(private _http: HttpClient) { }

authenticateCandidate(user: any): Observable<string> {
  console.log(user);
  return this._http.post<string>(this.candidateUrl, JSON.stringify(user),
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'//,
        // 'Accept': 'application/json'
      })
    });
}

authenticateRecruiter(user: any): Observable<string> {
  return this._http.post<string>(this.recruiterUrl, JSON.stringify(user),
    {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'//,
        // 'Accept': 'application/json'
      })
    });
}

getBearerToken() {
  alert("A");
  return localStorage.getItem("bearerToken");
}

setBearerToken(token: string) {
  localStorage.clear();
  localStorage.setItem("bearerToken", token);
}
}