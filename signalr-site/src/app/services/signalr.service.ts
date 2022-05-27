import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  connection?: signalR.HubConnection;

  private viewcountSubject$ = new BehaviorSubject<number>(0);
  viewcount$ = this.viewcountSubject$.asObservable();

  constructor() {
    this.InitWebSocket();
   }


  /**
   * Init connection
   */
  InitWebSocket(): void {

    this.connection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.api_url}/Notify/View`, {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    })
    .withAutomaticReconnect()
    .build();

    this.connection.start()
    .then(() => {
      console.log('Connection open');
      this.notify();
    })
    .catch((error) => console.log('Signalr connection error', error));

    if(this.connection)
      this.connection.on('BroadcastWatching', (count) => this.viewcountSubject$.next(count));   

  }

  notify() {
    this.connection?.send("NotifyWatching");
  }
}
