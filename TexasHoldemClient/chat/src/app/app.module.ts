import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AngularFireModule } from 'angularfire2';
import { FcmService } from "app/fcm-service";
import { HttpModule } from "@angular/http";

const config = {
  apiKey: "AIzaSyDNeJein6-7c543frBjRY-YMj30GV-9XZI",
  authDomain: "texasholdem-7ff59.firebaseapp.com",
  databaseURL: "https://texasholdem-7ff59.firebaseio.com",
  projectId: "texasholdem-7ff59",
  storageBucket: "texasholdem-7ff59.appspot.com",
  messagingSenderId: "989213145723"
};

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AngularFireModule.initializeApp(config)
  ],
  providers: [FcmService],
  bootstrap: [AppComponent]
})
export class AppModule { }
