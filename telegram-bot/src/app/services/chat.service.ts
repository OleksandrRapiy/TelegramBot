import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../models/message';
import { Receiver } from '../models/receiver';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private readonly Url: string = environment.domain + '/api/bot'

  constructor(private http: HttpClient) { }

  sendMessage(message: Message): Observable<void> {
    return this.http.post<void>(`${this.Url}/send-message`, message)
  }

  getReceivers(): Observable<Receiver[]> {
    return this.http.get<Receiver[]>(`${this.Url}/receivers`);
  }

  getMessages(chatId: number): Observable<Message[]> {
    return this.http.get<Message[]>(`${this.Url}/messages/${chatId}`)
  }
}
