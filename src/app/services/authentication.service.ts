import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginRequest } from '../models/LoginRequest';
import { Observable, catchError, throwError } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  
  constructor(private http: HttpClient) {
    
   }

 
  login(values: LoginRequest):Observable<any>{
    return this.http.post('https://mercadolibro-api.app.csharpjourney.com/api/User/login', values);
  }

 
}

