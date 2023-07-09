import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import {FormGroup, FormControl, Validators} from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  constructor(private router: Router){}

  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  }) 

  ngOnInit(): void{
  
  };
 

  login(){
    
    return '';

  }

}
