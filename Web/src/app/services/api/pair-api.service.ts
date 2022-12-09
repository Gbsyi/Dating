import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NextPairVm } from '../models/next-pair-vm';

@Injectable({
  providedIn: 'root',
})
export class PairApiService {
  constructor(
    @Inject(BASE_API_URL) private readonly baseUrl: string,
    private readonly http: HttpClient
  ) {}

  getNextPair(): Observable<NextPairVm> {
    return this.http.get<NextPairVm>(`${this.baseUrl}/pair/next`);
  }
}
