import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button'
import {MatFormFieldModule} from '@angular/material/form-field'
import {MatInputModule} from '@angular/material/input'
import { MatCardModule } from '@angular/material/card';



@NgModule({
  declarations: [],
  exports:[
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule

  ],
  imports: [
    CommonModule
  ]
})
export class MaterialModule { }
