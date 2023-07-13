import { Component, OnInit } from '@angular/core';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/LoginRequest';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  loginError: string="";
  constructor( private router: Router, private authenticationService: AuthenticationService){}
  ngOnInit(): void{
  
  };
  
 
  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  get email(){
    return this.loginForm.controls.email;
  }
  get password(){
    return this.loginForm.controls.password;
  }
 
  login(){
    if(this.loginForm.valid){
      this.authenticationService.login(this.loginForm.value as LoginRequest)
      .subscribe({next: () => this.router.navigateByUrl('/index')
      })
   
    }   
  }     
}
