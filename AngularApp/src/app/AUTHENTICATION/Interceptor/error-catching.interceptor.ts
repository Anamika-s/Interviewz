import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { LoginService } from "../Service/login.service";
import { Injectable } from "@angular/core";

@Injectable()
export class ErrorCatchingInterceptor 
implements HttpInterceptor {

  errorMsg : string="";
  constructor(private _loginservice : LoginService) {}

  intercept(request: HttpRequest<unknown>,
     next: HttpHandler): Observable<HttpEvent<unknown>> {
      const token = this._loginservice.getBearerToken();

      if (token) {
        alert("token");
        request = request.clone({
            setHeaders: {Authorization: `Bearer ${token}`}
        });
      } 
      console.log("Passed through the interceptor in request");
      return next.handle(request)
      .pipe(
        map(res => {
          console.log("Passed through the interceptor in response");
          return res
        }),

      catchError(err => {
        if (err.error instanceof ErrorEvent) {
            this.errorMsg = `Error: ${err.error.message}`;
        } 
        else {

            this.errorMsg = this.getServerErrorMessage(err);
        }

        return throwError(this.errorMsg);
    })
  );

  }

  private getServerErrorMessage(error: HttpErrorResponse)
  : string {

    switch (error.status) {

        case 404: {
            return `Not Found: ${error.message}`;
        }

        case 403: {
            return `Access Denied: ${error.message}`;
        }

        case 500: {
            return `Internal Server Error: ${error.error}`;
        }

        default: {
            return `Unknown Server Error: ${error.message}`;
        }
    }
  }
}