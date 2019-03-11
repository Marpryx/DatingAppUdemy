import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'; //import in order to use http client

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent //was added auto because of extensions generate
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule //needs to be imported in order to use httpClents services (to do get requests)
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
