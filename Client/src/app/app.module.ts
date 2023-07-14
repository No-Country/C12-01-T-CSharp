import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BookCardComponent } from './components/book-card/book-card.component';
import { BookDetailsComponent } from './components/book-details/book-details.component';
import { BookFilterComponent } from './components/book-filter/book-filter.component';
import { LoginComponent } from './components/login/login.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { HomeComponent } from './components/home/home.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ThemePickerComponent } from './components/theme-picker/theme-picker.component';
import { SimilarbooksComponent } from './components/similarbooks/similarbooks.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

import { HttpInterceptorService } from './interceptors/http-interceptor.service';
import { ErrorInterceptorService } from './interceptors/error-interceptor.service';

import { NgMaterialModule } from './ng-material/ng-material.module';
import { AppRoutingModule } from './app-routing.module';
import { PriceFilterComponent } from './components/price-filter/price-filter.component';
import { SearchComponent } from './components/search/search.component';

@NgModule({
  declarations: [
    AppComponent,
    BookCardComponent,
    BookDetailsComponent,
    BookFilterComponent,
    LoginComponent,
    UserRegistrationComponent,
    HomeComponent,
    NavBarComponent,
    ThemePickerComponent,
    SimilarbooksComponent,
    PageNotFoundComponent,
    PriceFilterComponent,
    SearchComponent,
  ],
  imports: [
    NgMaterialModule,
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }