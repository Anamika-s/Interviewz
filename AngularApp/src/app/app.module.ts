import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule, components } from './app-routing.module';
import { AppComponent } from './app.component';
import { TimeslotComponent } from './timeslot/timeslot.component';
import { HomepageComponent } from './homepage/homepage.component';
import { BookslotComponent } from './bookslot/bookslot.component';
import { DashboardComponent } from './CandidateLogin/Component/dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatFormFieldModule, matFormFieldAnimations } from '@angular/material/form-field';
import {MatTabsModule} from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './AUTHENTICATION/Component/login/login.component';
import { RegistrationComponent } from './AUTHENTICATION/Component/registration/registration.component';
import { LoginService } from './AUTHENTICATION/Service/login.service';
import { MatSelectModule } from '@angular/material/select';
import { RegistrationService } from './AUTHENTICATION/Service/registration.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import { ErrorCatchingInterceptor } from './AUTHENTICATION/Interceptor/error-catching.interceptor';
import { MatMenuModule } from '@angular/material/menu';

import { FeedbackComponent } from './feedback/feedback/feedback.component';
import { RecruiterDashboardComponent } from './recruiter-dashboard/recruiter-dashboard.component';
import { CreateTimeslotComponent } from './create-timeslot/create-timeslot.component';
import { DateFormatPipe } from './pipes/date-format.pipe';

@NgModule({
    declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    RegistrationComponent,
    TimeslotComponent,
    HomepageComponent,
    BookslotComponent,
    FeedbackComponent,
    RecruiterDashboardComponent,
    CreateTimeslotComponent,
    DateFormatPipe, 
  ],
    imports: [
    BrowserModule,
    FormsModule,
    MatSelectModule,
    HttpClientModule,
    MatMenuModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatCardModule,
    MatCheckboxModule,
    MatSlideToggleModule,
    MatIconModule,
    MatDividerModule,
    MatTabsModule,
    MatFormFieldModule,
    ReactiveFormsModule
  ],
  providers: [
    LoginService,
    RegistrationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorCatchingInterceptor,
      multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
