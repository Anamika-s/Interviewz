import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TimeslotComponent } from './timeslot/timeslot.component';
import { HomepageComponent } from './homepage/homepage.component';
import { BookslotComponent } from './bookslot/bookslot.component';
import { LoginComponent } from './AUTHENTICATION/Component/login/login.component';
import { RegistrationComponent } from './AUTHENTICATION/Component/registration/registration.component';
import { DashboardComponent } from './CandidateLogin/Component/dashboard/dashboard.component';
import { RecruiterDashboardComponent } from './recruiter-dashboard/recruiter-dashboard.component';
import { CreateTimeslotComponent } from './create-timeslot/create-timeslot.component';
import { AuthGuard } from './AUTHENTICATION/Gaurd/auth.guard';

const routes: Routes = [
  {
    path: 'candidatedashboard',
    canActivate: [AuthGuard],
    component: DashboardComponent
  },
  {
    path: 'bookslot',
    canActivate: [AuthGuard],
    component: BookslotComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegistrationComponent
  },
  {
    path: 'timeslot',
    component: TimeslotComponent
  },
  {
    path: 'recruiterdashboard',
    canActivate: [AuthGuard],
    component: RecruiterDashboardComponent
  },
  {
    path: 'create-timeslot',
    component: CreateTimeslotComponent
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomepageComponent
  },
  {
    path : '**',
    component : HomepageComponent
  }
]


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const components = [LoginComponent, RegistrationComponent]