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

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getMessages() {
    return [
      ...this.sentPings.map(msg => ({
        ...msg,
        source: 'You',
        type: 'Ping'
      })),
      ...this.receivedPongs.map(msg => ({
        ...msg,
        source: `Server (${this.baseUrl})`,
        type: 'Ping'
      }))
    ].sort((a, b) => a.messageId - b.messageId);
  }

  handlePong(pong: PingPongMessage) {
    this.receivedPongs.push(pong);
    setTimeout(() => {
      this.sendPing(pong.messageId + 1)
    }, PING_PONG_INTERVAL);
  }

  startPingPong() {
    this.connection = new signalR.HubConnectionBuilder().withUrl('/ping-pong').build();
    this.connection.on('pong', this.handlePong.bind(this));
    this.connection.start()
      .then(() => this.sendPing(this.sentPings.length + this.receivedPongs.length + 1))
      .catch(err => console.error('Error starting connection with /ping-pong', err));
  }

  sendPing(messageId) {
    const responsePing: PingPongMessage = {
      date: new Date().toISOString(),
      messageId: messageId
    }
    this.sentPings.push(responsePing);
    this.connection.send('ping', responsePing);
  }

  stopPingPong() {
    this.connection.stop();
  }

  getConnectionState() {
    return this.connection && this.connection.state;
  }
}

interface PingPongMessage {
  date: string;
  messageId: number;
}
