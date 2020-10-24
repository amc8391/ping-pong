import { Component, Inject } from '@angular/core';
import * as signalR from "@microsoft/signalr";

const PING_PONG_INTERVAL = 1000;

@Component({
  selector: 'app-ping-pong',
  templateUrl: './ping-pong.component.html'
})
export class PingPongComponent {
  public sentPings: PingPongMessage[] = [];
  public receivedPongs: PingPongMessage[] = [];

  private connection: signalR.HubConnection;
  private baseUrl: string;
  private sessionId: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getMessages() : any[] {
    return [
      ...this.sentPings.map(msg => ({ ...msg, source: 'You', type: 'Ping' })),
      ...this.receivedPongs.map(msg => ({ ...msg, source: `Server (${this.baseUrl})`, type: 'Ping' }))
    ].sort((a, b) => a.messageId - b.messageId);
  }

  handlePong(pong: PingPongMessage) : void {
    this.receivedPongs.push(pong);
    if (this.sessionId !== pong.sessionId) {
      this.sessionId = pong.sessionId;
    }
    setTimeout(() => {
      this.sendPing(this.getLastMessageId() + 1, pong.sessionId)
    }, PING_PONG_INTERVAL);
  }

  getLastMessageId() : number {
    return this.getMessages().slice(-1)[0] ? this.getMessages().slice(-1)[0].messageId : 0;
  }

  startPingPong() : void {
    this.connection = new signalR.HubConnectionBuilder().withUrl('/ping-pong').build();
    this.connection.on('pong', this.handlePong.bind(this));
    this.connection.start()
      .then(() => {
        this.sendPing(this.getLastMessageId() + 1, this.sessionId)
      })
      .catch(err => console.error('Error starting connection with /ping-pong', err));
  }

  sendPing(messageId: number, sessionId: string) : void {
    const responsePing: PingPongMessage = {
      date: new Date().toISOString(),
      messageId,
      sessionId
    }
    this.sentPings.push(responsePing);
    this.connection.send('ping', responsePing);
  }

  stopPingPong() : void {
    this.connection.stop();
  }

  getConnectionState() : string {
    return this.connection && this.connection.state;
  }
}

interface PingPongMessage {
  date: string;
  messageId: number;
  sessionId: string;
}
