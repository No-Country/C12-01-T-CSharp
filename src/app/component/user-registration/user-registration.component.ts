import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterRequest } from 'src/app/models/RegisterRequest';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent {


  constructor(private router: Router, private fb: FormBuilder, private userService: UserService){}

  registrationForm = this.fb.group({
    userName: ['', Validators.required],
    email: ['', Validators.required], 
    password: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required]
  }
  );
  get username() {
    return this.registrationForm.get('username');
  }

  get email() {
    return this.registrationForm.get('email');
  }

  get password() {
    return this.registrationForm.get('password');
  }

  get firstname() {
    return this.registrationForm.get('firstname');
  }
  get lastname() {
    return this.registrationForm.get('lastname');
  }
  
  registerUser(){
    if(this.registrationForm.valid){
      this.userService.registerUser(this.registrationForm.value as RegisterRequest)
      .subscribe({next: () => this.router.navigateByUrl('/login')
      },)

    }
  }
}
