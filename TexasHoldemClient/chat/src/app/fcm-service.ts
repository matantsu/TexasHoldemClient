import { Injectable, Inject } from "@angular/core";
import { Http } from "@angular/http";
import { FirebaseApp } from "angularfire2";
import * as firebase from 'firebase';


@Injectable()
export class FcmService {

    private messaging: firebase.messaging.Messaging;

    constructor(@Inject(FirebaseApp) private fbApp: firebase.app.App, private http: Http) {
        
        let messaging = firebase.messaging(fbApp);
        
        // messaging.requestPermission()
        //     .then(x => {alert(JSON.stringify({'success': x}));})
        //     .catch(x => {alert(JSON.stringify({'error': x}));});
        

        messaging.onMessage(()=>{alert('new message !!')});

        messaging.getToken()
            .then(function(currentToken) {
            if (currentToken) {
                console.log('token:', currentToken);
            } else {
                console.log('No Instance ID token available. Request permission to generate one.');
            }
            })
            .catch(function(err) {
                console.log('An error occurred while retrieving token. ', err);
            });
    }
}