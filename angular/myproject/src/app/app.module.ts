import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudentComponentComponent } from './Component/student-component/student-component.component';
import { FormsModule } from '@angular/forms';
import { UpdateComponentComponent } from './Component/update-component/update-component.component';

@NgModule({
  declarations: [
    AppComponent,
    StudentComponentComponent,
    UpdateComponentComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
