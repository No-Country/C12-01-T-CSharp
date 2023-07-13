import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { UserRegistrationComponent } from './component/user-registration/user-registration.component';


const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: UserRegistrationComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
