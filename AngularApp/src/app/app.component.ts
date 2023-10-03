import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'InterQ';
  role: any;
  
  recruiterDashboard: boolean = true;
  isRouteActive: any;

  isRecruiterActive = false;
  isCandidateActive = false;
  isBookingActive = false;
  isHomeActive = false;
  isCreateTimeslotActive =  false;
  isTimeslotActive = false;
  isLoggedOut = true;

  constructor(private router: Router, private route: ActivatedRoute){
    this.router.events.subscribe(() => {
      this.isRecruiterActive = this.router.url.includes('/recruiterdashboard');
      this.isCreateTimeslotActive = this.router.url.includes('/create-timeslot');
      this.isBookingActive = this.router.url.includes('/bookslot');
      this.isCandidateActive = this.router.url.includes('/candidatedashboard');
      this.isTimeslotActive = this.router.url.includes('/timeslot');
      this.isHomeActive = this.router.url.includes('/home');
    });
  }
  ngOnInit(): void {
  }
  
  handleLogin(loggedIn: boolean) {
    if (loggedIn) {
      this.role = localStorage.getItem('Role');
      if(this.role != 'Recruiter') this.recruiterDashboard = false;
      console.log('User is logged in.');
    }
  }

  openDashboard(): void {
    if (localStorage.getItem('Role') == null) {
      this.router.navigate(['/login']);
    } else {
      this.role = localStorage.getItem('Role');
      if (this.role == 'Recruiter') {
        this.router.navigate(['/recruiterdashboard']);
      } else {
        this.router.navigate(['/candidatedashboard']);
      }
      
    }
  }

  Logout(): void{
    this.isLoggedOut = true;
    localStorage.clear();
    this.router.navigate(['/home']);
  }

  userLoggedIn(loggedIn : boolean){
    this.isLoggedOut = false;
  }
}
