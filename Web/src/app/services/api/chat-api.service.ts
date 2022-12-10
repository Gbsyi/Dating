import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageVm } from '../models/message-vm';

@Injectable({
  providedIn: 'root',
})
export class ChatApiService {
  constructor(
    @Inject(BASE_API_URL) private readonly baseUrl: string,
    private readonly http: HttpClient
  ) {}

  public getMessages(chatId: string): Observable<MessageVm[]> {
    return this.http.get<MessageVm[]>(`${this.baseUrl}/chat/messages`, {
      params: {
        chatId: chatId,
      },
    });
  }

  sendMessage(chatId: string, message: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/chat/send-message`, {
      chatId,
      text: message,
    });
  }
}
