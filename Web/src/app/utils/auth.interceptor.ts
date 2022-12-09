import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private readonly router: Router) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const id = this.getId();
    let req: HttpRequest<unknown> = request;
    if (id) {
      req = request.clone({
        headers: request.headers.append('IdAuth', id),
      });
    }

    return next.handle(req).pipe(
      catchError(
        (
          httpErrorResponse: HttpErrorResponse,
          _: Observable<HttpEvent<any>>
        ) => {
          if (httpErrorResponse.status === HttpStatusCode.Unauthorized) {
            this.router.navigate(['/login']);
          }
          return throwError(() => httpErrorResponse);
        }
      )
    );
  }

  getId(): string | null {
    return localStorage.getItem('auth');
  }
}
