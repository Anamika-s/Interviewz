import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {


  constructor(private router : Router) { }

  ngOnInit(): void {
    
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
