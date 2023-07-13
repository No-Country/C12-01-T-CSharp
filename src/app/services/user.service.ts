import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/LoginRequest';
import { RegisterRequest } from '../models/RegisterRequest';
import { User } from '../models/user';
import { Observable, map, observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  
  constructor(private http: HttpClient) { 
    
  }

  registerUser(values: RegisterRequest):Observable<any>{
    return this.http.post('https://mercadolibro-api.app.csharpjourney.com/api/User', values);
  }
  
};
  
  


