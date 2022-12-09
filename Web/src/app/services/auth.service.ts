import { Injectable } from '@angular/core';
import { AuthApiService } from './api/auth-api.service';
import { catchError, map, Observable, throwError } from 'rxjs';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  get userId(): string | null {
    return localStorage.getItem('auth');
  }

  constructor(
    private readonly authApiService: AuthApiService,
    private readonly notificationService: NzNotificationService
  ) {}

  login(username: string, password: string): Observable<void> {
    return this.authApiService.loginPost({ username, password }).pipe(
      map((response) => {
        localStorage.setItem('auth', response.userId);
      }),
      catchError((err, caught) => {
        this.notificationService.error('Ошибка', err.message);
        return throwError(() => caught);
      })
    );
  }

  register(username: string, password: string): Observable<void> {
    return this.authApiService.registerPost({ username, password }).pipe(
      map((response) => {
        localStorage.setItem('auth', response.userId);
      }),
      catchError((err, caught) => {
        this.notificationService.error('Ошибка', err.message);
        return throwError(() => caught);
      })
    );
  }
}
