import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { LoginResultVm } from '../models/api-models';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  constructor(
    @Inject(BASE_API_URL) private readonly baseUrl: string,
    private readonly http: HttpClient
  ) {}

  loginPost(request: { username: string; password: string }) {
    return this.http.post<LoginResultVm>(
      `${this.baseUrl}/account/login`,
      request,
      {
        headers: {
          'Access-Control-Allow-Origin': '*',
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
      }
    );
  }

  registerPost(request: { username: string; password: string }) {
    return this.http.post<LoginResultVm>(
      `${this.baseUrl}/account/register`,
      request
    );
  }
}
