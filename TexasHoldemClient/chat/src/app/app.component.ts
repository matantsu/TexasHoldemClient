import { Component } from '@angular/core';
import { FcmService } from "app/fcm-service";

@Component({
  selector: 'app-root',
  template: `
    <div>
      hello
    </div>
  `,
  styles: []
})
export class AppComponent {
  constructor(private service: FcmService){}
}
