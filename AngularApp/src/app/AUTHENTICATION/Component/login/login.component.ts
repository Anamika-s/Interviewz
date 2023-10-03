import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { lastValueFrom, throwError } from 'rxjs';
import { LoginService } from '../../Service/login.service';
import { Router } from '@angular/router';
import { RegistrationService } from '../../Service/registration.service';
import { cachedDataVersionTag } from 'v8';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  @Output() loggedIn: EventEmitter<boolean> = new EventEmitter<boolean>();

  loginCandidateForm: FormGroup;
  loginRecruiterForm: FormGroup;
  candidate: any = {};
  recruiter: any = {};
  token: any = {};

  constructor(private fb: FormBuilder, private _loginservice: LoginService, private router: Router, private registrationServie: RegistrationService) {
    this.loginCandidateForm = fb.group({
      Email: new FormControl("", [Validators.required, Validators.email]),
      Password: new FormControl("", [Validators.required, Validators.minLength(3)])
    });
    this.loginRecruiterForm = fb.group({
      Email: new FormControl("", [Validators.required, Validators.email]),
      Password: new FormControl("", [Validators.required, Validators.minLength(3)])
    });
  }

  async loginCandidate(regForm: any) {
    if (this.loginCandidateForm.invalid) return;
    else {

      var user = {
        "Email": regForm.controls.Email.value,
        "Password": regForm.controls.Password.value
      };
      console.log(user);
      this._loginservice.authenticateCandidate(user).subscribe(
        async(res) => {
          this.token = (res);
          this._loginservice.setBearerToken(this.token.token);
          localStorage.setItem('Email', regForm.controls.Email.value);
          localStorage.setItem('Role', "Candidate");
          const candidates =await this.registrationServie.getAllCandidate().toPromise();
          const email = localStorage.getItem("Email");
          for (let candidate of candidates) {
            if (candidate.email == email) {
              localStorage.setItem('Id', candidate.candidateId);
              break;
            }
          }
          this.loggedIn.emit(true);
          this.router.navigate(['/candidatedashboard']);
        },
        (err: any) => {
          window.alert("Incorrect Credentials!!!")
        });
      try {
        this.token = await lastValueFrom(this._loginservice.authenticateCandidate(user));
      } catch (err) {
        throwError;
      }
    }
  }

  async loginRecruiter(regForm: any) {
    if (this.loginRecruiterForm.invalid) return;
    else {
      var user = {
        "Email": regForm.controls.Email.value,
        "Password": regForm.controls.Password.value
      };
      this._loginservice.authenticateRecruiter(user).subscribe(
        async (res) => {
          this.token = (res);
          this._loginservice.setBearerToken(this.token.token);
          localStorage.setItem('Email', regForm.controls.Email.value);
          localStorage.setItem('Role', "Recruiter");
          const recruiters =await this.registrationServie.getAllRecruiter().toPromise();
          const email = localStorage.getItem("Email");
          for (let recruiter of recruiters) {
            if (recruiter.email == email) {
              localStorage.setItem('Id', recruiter.id);
              break;
            }
          }
          this.loggedIn.emit(true);
          this.router.navigate(['/recruiterdashboard']);
        },      
        (err: any) => {
          window.alert("Incorrect Credentials!!!")
        });
      try {
        this.token = await lastValueFrom(this._loginservice.authenticateRecruiter(user));
      } catch (err) {
        throwError;
      }
    }
  }
}