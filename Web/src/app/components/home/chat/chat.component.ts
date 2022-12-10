import { Component, OnInit } from '@angular/core';
import { MessageVm } from '../../../services/models/message-vm';
import {
  BehaviorSubject,
  finalize,
  interval,
  Observable,
  switchMap,
  tap,
  timeInterval,
} from 'rxjs';
import { ChatApiService } from '../../../services/api/chat-api.service';
import { ActivatedRoute } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { FormControl } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ProfileService } from 'src/app/services/profile.service';
import { AuthService } from '../../../services/auth.service';

@UntilDestroy()
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.less'],
})
export class ChatComponent implements OnInit {
  messages$ = new BehaviorSubject<MessageVm[]>([]);
  messageSending$ = new BehaviorSubject<boolean>(false);
  messageText = new FormControl<string>('');
  chatId!: string;
  userId!: string;

  constructor(
    private readonly chatApiService: ChatApiService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly notificationService: NzNotificationService,
    private readonly authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.userId!;
    const chatId = this.activatedRoute.snapshot.paramMap.get('chatId');
    if (chatId) {
      this.chatId = chatId;
    } else {
      this.notificationService.error('Ошибка', 'Неизвестный чат');
    }
    const req = interval(1000);
    req
      .pipe(
        untilDestroyed(this),
        switchMap(() => this.loadMessages())
      )
      .subscribe();
  }

  loadMessages() {
    return this.chatApiService.getMessages(this.chatId).pipe(
      tap((messages) => {
        this.messages$.next(messages);
      })
    );
  }

  sendMessage() {
    this.messageSending$.next(true);
    const message = this.messageText.value;
    if (message) {
      this.messageText.reset();
      this.chatApiService
        .sendMessage(this.chatId, message)
        .pipe(
          finalize(() => this.messageSending$.next(false)),
          switchMap(() => this.loadMessages())
        )
        .subscribe();
    }
  }

  isUserMessage(userId: string) {
    return this.userId == userId;
  }
}
