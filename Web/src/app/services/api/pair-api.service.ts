import { Inject, Injectable } from '@angular/core';
import { BASE_API_URL } from './api-config';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NextPairVm } from '../models/next-pair-vm';
import { LikePairResultVm } from '../models/like-pair-result-vm';
import { PairVm } from '../models/pair-vm';

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

  likeProfile(profileId: string): Observable<LikePairResultVm> {
    return this.http.post<LikePairResultVm>(`${this.baseUrl}/pair/like`, {
      likedProfileId: profileId,
    });
  }

  dislikeProfile(profileId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/pair/dislike`, {
      likedProfileId: profileId,
    });
  }

  getUserPairs(): Observable<PairVm[]> {
    return this.http.get<PairVm[]>(`${this.baseUrl}/pair/pairs`);
  }
}
